using HTDal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();



            #region jwt
            var Issurer = "JWTBearer.Auth";  //������
            var Audience = "api.auth";       //������
            var secretCredentials = "q2xiARx$4x3TKqBJ";   //��Կ

            //������֤����
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,//������
                                          //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = Audience,//������
                                             //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretCredentials)),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��
                };
            });

            #endregion

            services.AddDbContext<CmDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
             // ���ʹ��SQL Server 2008���ݿ⣬�����UseRowNumberForPaging��ѡ��
             // �ο�����:https://github.com/aspnet/EntityFrameworkCore/issues/4616 
             // ����Ҫ˵����:2020-03-23���£�΢��ٷ����µ�Entity Framework Core�Ѳ���֧��UseRowNumberForPaging()���뾡��ʹ��SQL Server 2012 +�汾
             //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b=>b.UseRowNumberForPaging())
             );


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RBAC Management System API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });//https://localhost:44327/swagger/index.html

            // ע����־
            services.AddLogging(config =>
            {
                config.AddLog4Net();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //��֤ 
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseEndpoints(ep =>
            {
                //ep.MapControllers();
                ep.MapControllerRoute(name: "areaRoute", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                ep.MapControllerRoute(name: "apiDefault", pattern: "api/{controller=Home}/{action=Index}/{id?}");
                ep.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
