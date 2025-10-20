using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;
using System.Net;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KHQ.Srv.Services
{
    public class EmailsSrv : IEmailsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public EmailsSrv(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<int> AddAsync(EmailsDto entity)
        {
            var emailToBeAdded = _mapper.Map<Emails>(entity);
            await _unitOfWork.Repository<Emails>().AddAsync(emailToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                await SendEmailToAdminAsync(entity);
            }
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var emailToBeDeleted = await _unitOfWork.Repository<Emails>().GetByIdAsync(id);
            if (emailToBeDeleted != null)
            {
                _unitOfWork.Repository<Emails>().Delete(emailToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<EmailsVM>> GetAllAsync()
        {
            var brouchures = await _unitOfWork.Repository<Emails>().GetAllAsync();
            return _mapper.Map<IEnumerable<EmailsVM>>(brouchures);
        }

        public async Task<EmailsVM?> GetByIdAsync(Guid id)
        {
            var emailsData = await _unitOfWork.Repository<Emails>().GetByIdAsync(id);
            return _mapper.Map<EmailsVM>(emailsData);
        }

        public async Task<int> UpdateAsync(EmailsVM entity)
        {
            var emailToBeUpdated = _mapper.Map<Emails>(entity);
            _unitOfWork.Repository<Emails>().Update(emailToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        private async Task SendEmailToAdminAsync(EmailsDto dto)
        {
            try
            {

                var emailSettings = await _unitOfWork.Repository<EmailSettings>().Queryable().ToListAsync();
                EmailSettings settings = new EmailSettings();
                foreach (var item in emailSettings)
                {
                    settings.Regards = item.Regards;
                    settings.Email = item.Email;
                    settings.Body = item.Body;
                    settings.SupportTeam = item.SupportTeam;
                    settings.Subject = item.Subject;
                }
                var body = new StringBuilder();
                body.AppendLine(dto.Message);
                body.AppendLine();
                body.AppendLine($"--");
                body.AppendLine($"Name: {dto.Name}");
                body.AppendLine($"Phone: {dto.Phone}");
                body.AppendLine($"Email: {dto.SentEmail}");

                await SendEmailAsync(
                    from: _configuration["EmailFrom"],
                    to: _configuration["EmailTo"],
                    subject: dto.Subject ?? "New Contact Message",
                    body: body.ToString()
                );

                // Send confirmation to user
                var confirmation = new StringBuilder();
                confirmation.AppendLine($"Dear {dto.Name},");
                confirmation.AppendLine();
                confirmation.AppendLine(settings.Body);
                confirmation.AppendLine();
                confirmation.AppendLine(settings.Regards);
                confirmation.AppendLine(settings.SupportTeam);

                await SendEmailAsync(
                    from: settings.Email,
                    to: dto.SentEmail,
                    subject: settings.Subject,
                    body: confirmation.ToString()
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        private async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(from, _configuration["DisplayName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            mail.To.Add(to);

            using (var smtp = new SmtpClient("smtp.yourserver.com", 587))
            {
                smtp.Credentials = new NetworkCredential(_configuration["EmailUserName"], _configuration["EmailPassword"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }

    }
}
