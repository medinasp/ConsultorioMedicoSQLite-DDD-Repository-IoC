using Consultorio.Infra;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsultorioMedico.Aplicacao
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICadMedicosService, CadMedicosService>();
            services.AddScoped<ICadPacientesService, CadPacientesService>();
            services.AddScoped<IProntuariosService, ProntuariosService>();

            services.AddInfrastructure();

            return services;
        }
    }
}
