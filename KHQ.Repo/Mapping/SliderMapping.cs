using KHQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KHQ.Repo.Mapping
{
    public class SliderMapping : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            //builder.OwnsMany(x => x.SliderImages, slider =>
            //{
            //    slider.ToTable(typeof(SliderImages).Name.Pluralize());
            //    slider.HasKey(x => x.Id);
            //    slider.Property(x => x.SliderId).IsRequired();
            //    slider.Property(x => x.ImagePath).IsRequired();
            //});
        }
    }
}
