using Npgsql.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Coffee.QR_BackEnd.Startup
{
    public class PdfGenerator
    {

        private void Execute_PDF(object sender)
        {
           /* iTextSharp.text.Document document = new iTextSharp.text.Document();

            // Set up the output stream
            string filePath = "statistics.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

            // Open the document
            document.Open();

            // Add the title and subtitle
            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            Paragraph title = new Paragraph("Accommodation Report", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            iTextSharp.text.Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph subtitle = new Paragraph("Your tour bookings", subtitleFont);
            subtitle.Alignment = Element.ALIGN_CENTER;
            document.Add(subtitle);

            // Add a separator
            LineSeparator separator = new LineSeparator();
            document.Add(new Chunk(separator));

            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Paragraph organizedBy = new Paragraph();
            organizedBy.Alignment = Element.ALIGN_LEFT;
            organizedBy.Add(new Chunk("Organized by:\n", boldFont));
            organizedBy.Add(new Chunk("Booking\n", infoFont));
            organizedBy.Add(new Chunk("Novi Sad, Serbia, 21000\n", infoFont));
            organizedBy.Add(new Chunk("booking@gmail.com", infoFont));
            document.Add(organizedBy);
            // Add spacing after the "Organized by" section
            document.Add(new Paragraph("\n"));

            // Add the "Customer details" section
            Paragraph customerDetails = new Paragraph();
            customerDetails.Alignment = Element.ALIGN_RIGHT;
            customerDetails.Add(new Chunk("Customer details:\n", boldFont));

            // Retrieve the tourist object
            Owner owner = _ownerService.Get(MainWindow.LogInUser.Id);
            if (owner != null)
            {
                // Access the name and email properties of the tourist
                string ownerName = owner.Name + " " + owner.Surname;
                string ownerEmail = owner.Email;

                // Add the tourist name and email to the "Customer details" section
                customerDetails.Add(new Chunk(ownerName + "\n", infoFont));
                customerDetails.Add(new Chunk(ownerEmail + "\n", infoFont));
            }
            // Add the "Customer details" section above the "From: Start Date" section
            document.Add(customerDetails);

            // Add spacing before the "From: Start Date" section
            document.Add(new Paragraph("\n"));

            // Add the date range information
            Paragraph dateRange = new Paragraph();
            dateRange.Add(new Chunk("From: ", boldFont));
            dateRange.Add(new Chunk(StartDate.ToString("dd-MM-yyyy"), infoFont));
            dateRange.Add(new Chunk("\nTo: ", boldFont));
            dateRange.Add(new Chunk(EndDate.ToString("dd-MM-yyyy"), infoFont));
            document.Add(dateRange);

            // Add spacing after the date range
            document.Add(new Paragraph("\n"));

            // Add a new paragraph of text
            string s = "Your reservation report for the selected accommodation " + SelectedAccommodation.Name + " in year " + SelectedYear.ToString() + ".";
            Paragraph paragraph = new Paragraph(s);
            document.Add(paragraph);

            // Add two rows of space
            document.Add(new Paragraph("\n\n"));

            // Create the table
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;

            // Set the column widths
            float[] columnWidths = { 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
            table.SetWidths(columnWidths);

            // Add table headers
            PdfPCell headerCell = new PdfPCell();
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headerCell.Padding = 5;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;


            headerCell.Phrase = new Phrase("Month", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Reservation", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Cancelation", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Rescheduling", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Renovation advice", infoFont);
            table.AddCell(headerCell);



            // Get the tour booking data for the selected date range
            List<Reservation> resevations = _reservationService.GetAll().FindAll(r => r.StartDate >= StartDate && r.EndDate <= EndDate);

            // Add tour booking data to the table
            AccommodationStatisticMonthDTOs.Clear();
            foreach (AccommodationStatisticDTO a in _accommodationService.StatisticByMonthForAccommodation(SelectedAccommodation.Id, SelectedYear))
            {
                table.AddCell(new PdfPCell(new Phrase(a.Month, infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Reservations.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Cancelations.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Rescheduling.ToString(), infoFont)));
                table.AddCell(new PdfPCell(new Phrase(a.Renovations.ToString(), infoFont)));
            }

            // Add the table to the document
            document.Add(table);

            // Close the document
            document.Close();

            // Open the PDF document with the default application
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            Execute_VisibilityPDFCommand();
            LabelVisibility = "Hidden"; */
        }
    }
}
