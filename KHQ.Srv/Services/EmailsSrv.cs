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

namespace KHQ.Srv.Services
{
    public class EmailsSrv : IEmailsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmailsSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                var body = new StringBuilder();
                body.AppendLine(dto.Message);
                body.AppendLine();
                body.AppendLine($"--");
                body.AppendLine($"Name: {dto.Name}");
                body.AppendLine($"Phone: {dto.Phone}");
                body.AppendLine($"Email: {dto.SentEmail}");

                await SendEmailAsync(
                    from: "no-reply@khq.com",
                    to: "admin@khq.com",
                    subject: dto.Subject ?? "New Contact Message",
                    body: body.ToString()
                );

                // Send confirmation to user
                var confirmation = new StringBuilder();
                confirmation.AppendLine($"Dear {dto.Name},");
                confirmation.AppendLine();
                confirmation.AppendLine("Thank you for contacting KHQ. We’ve received your message and will get back to you soon.");
                confirmation.AppendLine();
                confirmation.AppendLine("Best regards,");
                confirmation.AppendLine("KHQ Support Team");

                await SendEmailAsync(
                    from: "no-reply@khq.com",
                    to: dto.SentEmail,
                    subject: "We received your message",
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
                From = new MailAddress(from, "KHQ Website"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            mail.To.Add(to);

            using (var smtp = new SmtpClient("smtp.yourserver.com", 587))
            {
                smtp.Credentials = new NetworkCredential("your-smtp-username", "your-smtp-password");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }

    }
}
