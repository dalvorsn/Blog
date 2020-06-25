using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Categoria;
using Blog.Models.Blog.Etiqueta;
using Blog.Models.Blog.Postagem;
using Blog.Models.Blog.Postagem.Revisao;
using Blog.Models.Blog.Postagem.Revisao.Comentario;
using Blog.Models.ControleDeAcesso;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog
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
            services.AddControllersWithViews();

            using ( var database = new Database())
            {
                //database.Database.EnsureDeleted();
                
                database.Database.EnsureCreated();
                //database.CreateFakeData();
            }

            services.AddIdentity<Usuario, Papel>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<Database>()
                .AddErrorDescriber<DescritorDeErros>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/acesso/login";
            });

            services.AddDbContext<Database>();

            services.AddTransient<ControleDeAcessoService>();
            services.AddTransient<CategoriaOrmService>();
            services.AddTransient<PostagemOrmService>();
            services.AddTransient<AutorOrmService>();
            services.AddTransient<EtiquetaOrmService>();
            services.AddTransient<RevisaoOrmService>();
            services.AddTransient<ComentarioOrmService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "admin.categorias",
                    pattern: "{controller=AdminCategorias}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "admin.etiquetas",
                    pattern: "{controller=AdminEtiqueta}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "admin.autor",
                    pattern: "{controller=AdminAutor}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "admin.postagem",
                    pattern: "{controller=AdminPostagem}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "controleDeAcesso",
                    pattern: "acesso/{action}",
                    defaults: new { controller = "ControleDeAcesso", action = "Login" }
                );
            });
        }
    }
}
