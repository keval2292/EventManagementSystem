using Document = iTextSharp.text.Document;
using EMS.DB.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using System;
using System.IO;

namespace EventMentorSystem.Data
{
    public class PDFGenerator
    {
        public void Downloadpdf(Payment payment, IJSRuntime js, string filename = "EventPayment.pdf")
        {
            if (payment is null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            js.InvokeVoidAsync("DownloadPdf", filename, Convert.ToBase64String(PDFReports(payment)));
        }
        private byte[] PDFReports(Payment payment)
        {
            var memoryStream = new MemoryStream();
            float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBotton = 0.7f;

            Document pdf = new Document(
                PageSize.A4,
                margeLeft.ToDpi(),
                margeRight.ToDpi(),
                margeTop.ToDpi(),
                margeBotton.ToDpi()
                );

            pdf.AddTitle("payment Receipt");
            pdf.AddAuthor("Event Mentor");
            pdf.AddCreationDate();
            pdf.AddKeywords("Payment");
            pdf.AddSubject("Event Payment Receipt");



            PdfWriter writer = PdfWriter.GetInstance(pdf, memoryStream);
            var labelHeader = new Chunk("Event Mentor", FontFactory.GetFont("Arial", 16, BaseColor.White));

            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(89, 89, 226),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            pdf.Header = header;
            var labelFooter = new Chunk("Page", FontFactory.GetFont("Arial", 12, BaseColor.White));

            HeaderFooter Footer = new HeaderFooter(new Phrase(labelFooter), true)
            {
                BackgroundColor = new BaseColor(89, 89, 226),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            pdf.Footer = Footer;
            pdf.Open();

            var titel = new Paragraph("Payment Receipt", new Font(Font.HELVETICA, 13, Font.UNDERLINE))
            {
                Alignment = Element.ALIGN_CENTER
            };
            titel.SpacingAfter = 25f;
            pdf.Add(titel);
            var data = new Paragraph("Date :" + DateTime.Today.ToLongDateString()) { 
                Alignment=Element.ALIGN_RIGHT
            };
            pdf.Add(data);
            var line = new Paragraph("Event Name :" + payment.Event.EventName);
            line.SpacingAfter = 2f;
            pdf.Add(line);
            var line2 = new Paragraph("Organizer Name :" + payment.Event.OrganizerName);
            line2.SpacingAfter = 2f;
            pdf.Add(line2);
            var line3 = new Paragraph("Organizer Contact :" + payment.Event.OrganizerContact);
            line3.SpacingAfter = 2f;
            pdf.Add(line3);
            var line5 = new Paragraph("Payment Mode :" + payment.PaymentMode);
            line5.SpacingAfter = 2f;
            pdf.Add(line5);
            var line6 = new Paragraph("Transaction Id :" + payment.Transactionid);
            line6.SpacingAfter = 2f;
            pdf.Add(line6);
            var line7 = new Paragraph("Payment Date:" + payment.CreatedOn.ToLongDateString());
            line7.SpacingAfter = 30f;
            pdf.Add(line7);

            PdfPTable table = new PdfPTable(2);
            PdfPCell raw = new PdfPCell(new Phrase("Payment Details", FontFactory.GetFont("Arial", 10, BaseColor.White)))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BackgroundColor = new BaseColor(89, 89, 226)
            };
            PdfPCell raw1 = new PdfPCell(new Phrase("  "))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            PdfPCell raw2 = new PdfPCell(new Phrase("Amount(In Rupee)"))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            PdfPCell raw3 = new PdfPCell(new Phrase("Total Amount"))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            PdfPCell raw4 = new PdfPCell(new Phrase(ValidCurrency(payment.TotalAmount)))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            PdfPCell raw5 = new PdfPCell(new Phrase("Received Amount"))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            PdfPCell raw6 = new PdfPCell(new Phrase(ValidCurrency(payment.ReceivedAmount)))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            PdfPCell raw7 = new PdfPCell(new Phrase("Remaining Amount"))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            
            PdfPCell raw8 = new PdfPCell(new Phrase(ValidCurrency(payment.RemainingAmount)))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            raw.Colspan = 2;
            table.AddCell(raw);
            table.AddCell(raw1);
            table.AddCell(raw2);
            table.AddCell(raw3);
            table.AddCell(raw4);
            table.AddCell(raw5);
            table.AddCell(raw6);
            table.AddCell(raw7);
            table.AddCell(raw8);
            table.SpacingAfter = 30f;
            pdf.Add(table);

            var TC = new Paragraph("term and condition", new Font(Font.HELVETICA, 10, Font.BOLD));
            var TC2 = new Paragraph("(1)Payment once paid will not be refunded under any circumstances", new Font(Font.HELVETICA, 10, Font.NORMAL));
            var TC3 = new Paragraph("(2)Any request for refund of Payment will not be entertained.", new Font(Font.HELVETICA, 10, Font.NORMAL));
            var TC4 = new Paragraph("(3)This document is electronically generated & need not be signed.", new Font(Font.HELVETICA, 10, Font.NORMAL));
            var TC5 = new Paragraph("(4)This receipt must be produced when required.", new Font(Font.HELVETICA, 10, Font.NORMAL));
            pdf.Add(TC);
            pdf.Add(TC2);
            pdf.Add(TC3);
            pdf.Add(TC4);
            pdf.Add(TC5);


            margeLeft = margeLeft + 3.5f;
            margeRight = margeRight + 3.5f;
            margeTop = margeTop + 3.5f;
            margeBotton = margeBotton + 3.5f;
            PdfContentByte content = writer.DirectContent;
            Rectangle rectangle = new Rectangle(pdf.PageSize);
            rectangle.Left += margeLeft ;
            rectangle.Right -= margeRight;
            rectangle.Top -= margeTop;
            rectangle.Bottom += margeBotton;
            content.SetColorStroke(BaseColor.Black);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();

            pdf.Close();
            return memoryStream.ToArray();
        }

        string ValidCurrency(string value) => string.Format("₹ {0:#.00}", Convert.ToDecimal(value));
    }
}
