Imports DevExpress.XtraScheduler
Imports System
Imports System.Windows.Forms

Namespace SchedulerDbExample
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' TODO: This line of code loads data into the 'schedulerTestDataSet.Resources' table. You can move, or remove it, as needed.
			Me.resourcesTableAdapter.Fill(Me.schedulerTestDataSet.Resources)
			' TODO: This line of code loads data into the 'schedulerTestDataSet.Appointments' table. You can move, or remove it, as needed.
			Me.appointmentsTableAdapter.Fill(Me.schedulerTestDataSet.Appointments)

			schedulerControl1.Start = Date.Today
			schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day
			schedulerControl1.DayView.TopRowTime = New TimeSpan(10, 0, 0)
			schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
			schedulerControl1.DayView.ResourcesPerPage = 2
			schedulerControl1.DayView.TimeIndicatorDisplayOptions.ShowOverAppointment = True


			AddHandler Me.schedulerDataStorage1.AppointmentsChanged, AddressOf OnAppointmentChangedInsertedDeleted
			AddHandler Me.schedulerDataStorage1.AppointmentsInserted, AddressOf OnAppointmentChangedInsertedDeleted
			AddHandler Me.schedulerDataStorage1.AppointmentsDeleted, AddressOf OnAppointmentChangedInsertedDeleted
		End Sub

		Private Sub OnAppointmentChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			appointmentsTableAdapter.Update(schedulerTestDataSet)
			schedulerTestDataSet.AcceptChanges()
		End Sub

		Private Sub schedulerControl1_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs) Handles schedulerControl1.EditAppointmentFormShowing
			Dim scheduler As DevExpress.XtraScheduler.SchedulerControl = (DirectCast(sender, DevExpress.XtraScheduler.SchedulerControl))
			Dim form As New SchedulerDbExample.CustomAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm)
			Try
				e.DialogResult = form.ShowDialog()
				e.Handled = True
			Finally
				form.Dispose()
			End Try

		End Sub
	End Class
End Namespace
