using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecast.Core.Models;

namespace WeatherForecast.Data.Configurations
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder
                .HasKey(wf => wf.Id);

            builder
                .Property(wf => wf.Date)
                .IsRequired();

            builder
                .Property(wf => wf.TemperatureC)
                .IsRequired();

            builder
                .Property(wf => wf.TemperatureF);

            builder
                .Property(wf => wf.Summary)
                .IsRequired();

            builder
                .ToTable("WeatherForecasts");
        }
    }
}
