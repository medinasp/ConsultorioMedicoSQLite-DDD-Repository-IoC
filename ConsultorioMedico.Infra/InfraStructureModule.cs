using Microsoft.Extensions.DependencyInjection;
using ConsultorioMedico.Infra.Repositories;

namespace ConsultorioMedico.Infra
{
    public static class InfraStructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddInfraRepositories();

            return services;
        }

        private static IServiceCollection AddInfraRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICadPacientesRepository, CadPacientesRepository>();
            services.AddScoped<ICadMedicosRepository, CadMedicosRepository>();
            services.AddScoped<IProntuariosRepository, ProntuariosRepository>();

            return services;
        }
    }
}