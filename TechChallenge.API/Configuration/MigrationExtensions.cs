using Microsoft.EntityFrameworkCore;
using System;
using TechChallenge.Infrastracture.Data;

namespace TechChallenge.API.Configuration
{
	public static class MigrationExtensions
	{
		public static WebApplication ApplyMigrations(this WebApplication app)
		{
			if (app.Environment.EnvironmentName is "Local") return app;

			var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dataContext = scope.ServiceProvider.GetRequiredService<FiapDbContext>();

			dataContext.Database.Migrate();

			return app;
		}
	}
}
