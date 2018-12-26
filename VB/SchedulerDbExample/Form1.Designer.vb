Namespace SchedulerDbExample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
			Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
			Dim timeRuler3 As New DevExpress.XtraScheduler.TimeRuler()
			Me.schedulerDataStorage1 = New DevExpress.XtraScheduler.SchedulerDataStorage(Me.components)
			Me.appointmentsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.schedulerTestDataSet = New SchedulerDbExample.SchedulerTestDataSet()
			Me.resourcesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.appointmentsTableAdapter = New SchedulerDbExample.SchedulerTestDataSetTableAdapters.AppointmentsTableAdapter()
			Me.resourcesTableAdapter = New SchedulerDbExample.SchedulerTestDataSetTableAdapters.ResourcesTableAdapter()
			Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
			CType(Me.schedulerDataStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.appointmentsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.schedulerTestDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.resourcesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' schedulerDataStorage1
			' 
			Me.schedulerDataStorage1.Appointments.CustomFieldMappings.Add(New DevExpress.XtraScheduler.AppointmentCustomFieldMapping("Contacts", "CustomField1"))
			Me.schedulerDataStorage1.Appointments.DataSource = Me.appointmentsBindingSource
			Me.schedulerDataStorage1.Appointments.Mappings.AllDay = "AllDay"
			Me.schedulerDataStorage1.Appointments.Mappings.Description = "Description"
			Me.schedulerDataStorage1.Appointments.Mappings.End = "EndDate"
			Me.schedulerDataStorage1.Appointments.Mappings.Label = "Label"
			Me.schedulerDataStorage1.Appointments.Mappings.Location = "Location"
			Me.schedulerDataStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
			Me.schedulerDataStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
			Me.schedulerDataStorage1.Appointments.Mappings.ResourceId = "ResourceID"
			Me.schedulerDataStorage1.Appointments.Mappings.Start = "StartDate"
			Me.schedulerDataStorage1.Appointments.Mappings.Status = "Status"
			Me.schedulerDataStorage1.Appointments.Mappings.Subject = "Subject"
			Me.schedulerDataStorage1.Appointments.Mappings.TimeZoneId = "TimeZoneId"
			Me.schedulerDataStorage1.Appointments.Mappings.Type = "Type"
			Me.schedulerDataStorage1.Resources.DataSource = Me.resourcesBindingSource
			Me.schedulerDataStorage1.Resources.Mappings.Caption = "ResourceName"
			Me.schedulerDataStorage1.Resources.Mappings.Color = "Color"
			Me.schedulerDataStorage1.Resources.Mappings.Id = "ResourceID"
			Me.schedulerDataStorage1.Resources.Mappings.Image = "Image"
			' 
			' appointmentsBindingSource
			' 
			Me.appointmentsBindingSource.DataMember = "Appointments"
			Me.appointmentsBindingSource.DataSource = Me.schedulerTestDataSet
			' 
			' schedulerTestDataSet
			' 
			Me.schedulerTestDataSet.DataSetName = "SchedulerTestDataSet"
			Me.schedulerTestDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
			' 
			' resourcesBindingSource
			' 
			Me.resourcesBindingSource.DataMember = "Resources"
			Me.resourcesBindingSource.DataSource = Me.schedulerTestDataSet
			' 
			' appointmentsTableAdapter
			' 
			Me.appointmentsTableAdapter.ClearBeforeFill = True
			' 
			' resourcesTableAdapter
			' 
			Me.resourcesTableAdapter.ClearBeforeFill = True
			' 
			' schedulerControl1
			' 
			Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.schedulerControl1.Location = New System.Drawing.Point(0, 0)
			Me.schedulerControl1.Name = "schedulerControl1"
			Me.schedulerControl1.Size = New System.Drawing.Size(784, 561)
			Me.schedulerControl1.Start = New Date(2016, 2, 3, 0, 0, 0, 0)
			Me.schedulerControl1.DataStorage = Me.schedulerDataStorage1
			Me.schedulerControl1.TabIndex = 0
			Me.schedulerControl1.Text = "schedulerControl1"
			Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
			Me.schedulerControl1.Views.FullWeekView.Enabled = True
			Me.schedulerControl1.Views.FullWeekView.TimeRulers.Add(timeRuler2)
			Me.schedulerControl1.Views.WeekView.Enabled = False
			Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler3)
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.schedulerControl1.EditAppointmentFormShowing += new DevExpress.XtraScheduler.AppointmentFormEventHandler(this.schedulerControl1_EditAppointmentFormShowing);
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(784, 561)
			Me.Controls.Add(Me.schedulerControl1)
			Me.Name = "Form1"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "How to create a Custom Appointment Edit Form"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.Form1_Load);
			CType(Me.schedulerDataStorage1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.appointmentsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.schedulerTestDataSet, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.resourcesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region
		Private schedulerDataStorage1 As DevExpress.XtraScheduler.SchedulerDataStorage
		Private schedulerTestDataSet As SchedulerTestDataSet
		Private appointmentsBindingSource As System.Windows.Forms.BindingSource
		Private appointmentsTableAdapter As SchedulerTestDataSetTableAdapters.AppointmentsTableAdapter
		Private resourcesBindingSource As System.Windows.Forms.BindingSource
		Private resourcesTableAdapter As SchedulerTestDataSetTableAdapters.ResourcesTableAdapter
		Private WithEvents schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
	End Class
End Namespace

