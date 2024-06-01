using NeuroSpec.Shared.Models.DTO;
using System.Diagnostics;

namespace NeuroSpecCompanion.Views.MedicalHistory;
public partial class ViewMedicalRecord : ContentPage, IQueryAttributable
{
    public ViewMedicalRecord()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Record", out var record))
        {
            BindingContext = record as MedicalRecord;
            DisplayRecord(record as MedicalRecord);
        }
    }

    private void DisplayRecord(MedicalRecord record)
    {
        if (record != null && record.VisualAttachments != null && record.VisualAttachments.Count > 0)
        {
            var attachment = record.VisualAttachments[0];
            if (attachment.contentType.Contains("pdf", StringComparison.OrdinalIgnoreCase))
            {

                RecordPdf.Source = attachment.url;
                RecordPdf.IsVisible = true;
            }
            else
            {
                Console.WriteLine(attachment.url);
                RecordImage.Source = ImageSource.FromUri(new Uri(attachment.url));
                RecordImage.IsVisible = true;
            }
        }
    }
}
