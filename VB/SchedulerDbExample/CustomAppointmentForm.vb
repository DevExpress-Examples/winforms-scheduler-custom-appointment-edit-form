Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Reflection
Imports System.Windows.Forms
Imports DevExpress.Utils.Controls
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Localization
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.XtraScheduler.UI
Imports DevExpress.Utils
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors.Native
Imports DevExpress.Utils.Internal
Imports System.Collections.Generic

Namespace SchedulerDbExample

    Public Partial Class CustomAppointmentForm
        Inherits XtraForm
        Implements IDXManagerPopupMenu

#Region "Fields"
        Private openRecurrenceFormField As Boolean

        Private ReadOnly storageField As ISchedulerStorage

        Private ReadOnly controlField As SchedulerControl

        Private recurringIconField As Icon

        Private normalIconField As Icon

        Private ReadOnly controllerField As AppointmentFormController

        Private menuManagerField As IDXMenuManager

#End Region
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
            Me.New(control, apt, False)
        End Sub

        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment, ByVal openRecurrenceForm As Boolean)
            Guard.ArgumentNotNull(control, "control")
            Guard.ArgumentNotNull(control.Storage, "control.Storage")
            Guard.ArgumentNotNull(apt, "apt")
            openRecurrenceFormField = openRecurrenceForm
            controllerField = CreateController(control, apt)
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
            SetupPredefinedConstraints()
            LoadIcons()
            controlField = control
            storageField = control.Storage
            edtShowTimeAs.Storage = storageField
            edtLabel.Storage = storageField
            edtResource.SchedulerControl = control
            edtResource.Storage = storageField
            edtResources.SchedulerControl = control
            SubscribeControllerEvents(Controller)
            BindControllerToControls()
        End Sub

#Region "Properties"
        Protected Overrides ReadOnly Property ShowMode As FormShowMode
            Get
                Return FormShowMode.AfterInitialization
            End Get
        End Property

        Public Property MenuManager As IDXMenuManager
            Get
                Return menuManagerField
            End Get

            Private Set(ByVal value As IDXMenuManager)
                menuManagerField = value
            End Set
        End Property

        Protected Friend ReadOnly Property Controller As AppointmentFormController
            Get
                Return controllerField
            End Get
        End Property

        Protected Friend ReadOnly Property Control As SchedulerControl
            Get
                Return controlField
            End Get
        End Property

        Protected Friend ReadOnly Property Storage As ISchedulerStorage
            Get
                Return storageField
            End Get
        End Property

        Protected Friend ReadOnly Property IsNewAppointment As Boolean
            Get
                Return If(controllerField IsNot Nothing, controllerField.IsNewAppointment, True)
            End Get
        End Property

        Protected Friend ReadOnly Property RecurringIcon As Icon
            Get
                Return recurringIconField
            End Get
        End Property

        Protected Friend ReadOnly Property NormalIcon As Icon
            Get
                Return normalIconField
            End Get
        End Property

        Protected Friend ReadOnly Property OpenRecurrenceForm As Boolean
            Get
                Return openRecurrenceFormField
            End Get
        End Property

        Public Property [ReadOnly] As Boolean
            Get
                Return Controller IsNot Nothing AndAlso Controller.ReadOnly
            End Get

            Set(ByVal value As Boolean)
                If Controller.ReadOnly = value Then Return
                Controller.ReadOnly = value
            End Set
        End Property

#End Region
#Region "#CustomFieldData"
        Private _contacts As String

        Public Overridable Sub LoadFormData(ByVal appointment As Appointment)
            If appointment.CustomFields("Contacts") Is Nothing Then
                mxContacts.Text = ""
            Else
                _contacts = appointment.CustomFields("Contacts").ToString()
                mxContacts.Text = _contacts
            End If
        End Sub

        Public Overridable Function SaveFormData(ByVal appointment As Appointment) As Boolean
            appointment.CustomFields("Contacts") = mxContacts.Text
            Return True
        End Function

        Public Overridable Function IsAppointmentChanged(ByVal appointment As Appointment) As Boolean
            If Equals(_contacts, appointment.CustomFields("Contacts").ToString()) Then
                Return False
            Else
                Return True
            End If
        End Function

#End Region  ' #CustomFieldData
        Public Overridable Sub SetMenuManager(ByVal menuManager As IDXMenuManager)
            Call MenuManagerUtils.SetMenuManager(Controls, menuManager)
            menuManagerField = menuManager
        End Sub

        Protected Friend Overridable Sub SetupPredefinedConstraints()
            tbProgress.Properties.Minimum = AppointmentProcessValues.Min
            tbProgress.Properties.Maximum = AppointmentProcessValues.Max
            tbProgress.Properties.SmallChange = AppointmentProcessValues.Step
            edtResources.Visible = True
        End Sub

        Protected Overridable Sub BindControllerToControls()
            BindControllerToIcon()
            BindProperties(tbSubject, "Text", "Subject")
            BindProperties(tbLocation, "Text", "Location")
            BindProperties(tbDescription, "Text", "Description")
            BindProperties(edtShowTimeAs, "Status", "Status")
            BindProperties(edtStartDate, "EditValue", "DisplayStartDate")
            BindProperties(edtStartDate, "Enabled", "IsDateTimeEditable")
            BindProperties(edtStartTime, "EditValue", "DisplayStartTime")
            BindProperties(edtStartTime, "Visible", "IsTimeVisible")
            BindProperties(edtStartTime, "Enabled", "IsTimeVisible")
            BindProperties(edtEndDate, "EditValue", "DisplayEndDate", DataSourceUpdateMode.Never)
            BindProperties(edtEndDate, "Enabled", "IsDateTimeEditable", DataSourceUpdateMode.Never)
            BindProperties(edtEndTime, "EditValue", "DisplayEndTime", DataSourceUpdateMode.Never)
            BindProperties(edtEndTime, "Visible", "IsTimeVisible", DataSourceUpdateMode.Never)
            BindProperties(edtEndTime, "Enabled", "IsTimeVisible", DataSourceUpdateMode.Never)
            BindProperties(chkAllDay, "Checked", "AllDay")
            BindProperties(chkAllDay, "Enabled", "IsDateTimeEditable")
            BindProperties(edtResource, "ResourceId", "ResourceId")
            BindProperties(edtResource, "Enabled", "CanEditResource")
            BindToBoolPropertyAndInvert(edtResource, "Visible", "ResourceSharing")
            BindProperties(edtResources, "ResourceIds", "ResourceIds")
            BindProperties(edtResources, "Visible", "ResourceSharing")
            BindProperties(edtResources, "Enabled", "CanEditResource")
            BindProperties(lblResource, "Enabled", "CanEditResource")
            BindProperties(edtLabel, "Label", "Label")
            BindProperties(chkReminder, "Enabled", "ReminderVisible")
            BindProperties(chkReminder, "Visible", "ReminderVisible")
            BindProperties(chkReminder, "Checked", "HasReminder")
            BindProperties(cbReminder, "Enabled", "HasReminder")
            BindProperties(cbReminder, "Visible", "ReminderVisible")
            BindProperties(cbReminder, "Duration", "ReminderTimeBeforeStart")
            BindProperties(tbProgress, "Value", "PercentComplete")
            BindProperties(lblPercentCompleteValue, "Text", "PercentComplete", New ConvertEventHandler(AddressOf ObjectToStringConverter))
            BindProperties(progressPanel, "Visible", "ShouldEditTaskProgress")
            BindToBoolPropertyAndInvert(btnOk, "Enabled", "ReadOnly")
            BindToBoolPropertyAndInvert(btnRecurrence, "Enabled", "ReadOnly")
            BindProperties(btnDelete, "Enabled", "CanDeleteAppointment")
            BindProperties(btnRecurrence, "Visible", "ShouldShowRecurrenceButton")
        End Sub

        Protected Overridable Sub BindControllerToIcon()
            Dim binding As Binding = New Binding("Icon", Controller, "AppointmentType")
            AddHandler binding.Format, AddressOf AppointmentTypeToIconConverter
            DataBindings.Add(binding)
        End Sub

        Protected Overridable Sub ObjectToStringConverter(ByVal o As Object, ByVal e As ConvertEventArgs)
            e.Value = e.Value.ToString()
        End Sub

        Protected Overridable Sub AppointmentTypeToIconConverter(ByVal o As Object, ByVal e As ConvertEventArgs)
            Dim type As AppointmentType = CType(e.Value, AppointmentType)
            If type = AppointmentType.Pattern Then
                e.Value = RecurringIcon
            Else
                e.Value = NormalIcon
            End If
        End Sub

        Protected Overridable Sub BindProperties(ByVal target As Control, ByVal targetProperty As String, ByVal sourceProperty As String)
            BindProperties(target, targetProperty, sourceProperty, DataSourceUpdateMode.OnPropertyChanged)
        End Sub

        Protected Overridable Sub BindProperties(ByVal target As Control, ByVal targetProperty As String, ByVal sourceProperty As String, ByVal updateMode As DataSourceUpdateMode)
            target.DataBindings.Add(targetProperty, Controller, sourceProperty, True, updateMode)
        End Sub

        Protected Overridable Sub BindProperties(ByVal target As Control, ByVal targetProperty As String, ByVal sourceProperty As String, ByVal objectToStringConverter As ConvertEventHandler)
            Dim binding As Binding = New Binding(targetProperty, Controller, sourceProperty, True)
            AddHandler binding.Format, objectToStringConverter
            target.DataBindings.Add(binding)
        End Sub

        Protected Overridable Sub BindToBoolPropertyAndInvert(ByVal target As Control, ByVal targetProperty As String, ByVal sourceProperty As String)
            target.DataBindings.Add(New BoolInvertBinding(targetProperty, Controller, sourceProperty))
        End Sub

        Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            MyBase.OnLoad(e)
            If Controller Is Nothing Then Return
            DataBindings.Add("Text", Controller, "Caption")
            SubscribeControlsEvents()
            LoadFormData(Controller.EditedAppointmentCopy)
            RecalculateLayoutOfControlsAffectedByProgressPanel()
        End Sub

        Protected Overridable Function CreateController(ByVal control As SchedulerControl, ByVal apt As Appointment) As AppointmentFormController
            Return New AppointmentFormController(control, apt)
        End Function

        Private Sub SubscribeControllerEvents(ByVal controller As AppointmentFormController)
            If controller Is Nothing Then Return
            AddHandler controller.PropertyChanged, AddressOf OnControllerPropertyChanged
        End Sub

        Private Sub OnControllerPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If Equals(e.PropertyName, "ReadOnly") Then UpdateReadonly()
        End Sub

        Protected Overridable Sub UpdateReadonly()
            If Controller Is Nothing Then Return
            Dim controls As IList(Of Control) = GetAllControls(Me)
            For Each control As Control In controls
                Dim editor As BaseEdit = TryCast(control, BaseEdit)
                If editor Is Nothing Then Continue For
                editor.ReadOnly = Controller.ReadOnly
            Next

            btnOk.Enabled = Not Controller.ReadOnly
            btnRecurrence.Enabled = Not Controller.ReadOnly
        End Sub

        Private Function GetAllControls(ByVal rootControl As Control) As List(Of Control)
            Dim result As List(Of Control) = New List(Of Control)()
            For Each control As Control In rootControl.Controls
                result.Add(control)
                Dim childControls As IList(Of Control) = GetAllControls(control)
                result.AddRange(childControls)
            Next

            Return result
        End Function

        Protected Friend Overridable Sub LoadIcons()
            Dim asm As Assembly = GetType(SchedulerControl).Assembly
            recurringIconField = ResourceImageHelper.CreateIconFromResources(SchedulerIconNames.RecurringAppointment, asm)
            normalIconField = ResourceImageHelper.CreateIconFromResources(SchedulerIconNames.Appointment, asm)
        End Sub

        Protected Friend Overridable Sub SubscribeControlsEvents()
            AddHandler edtEndDate.Validating, New CancelEventHandler(AddressOf OnEdtEndDateValidating)
            AddHandler edtEndDate.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnEdtEndDateInvalidValue)
            AddHandler edtEndTime.Validating, New CancelEventHandler(AddressOf OnEdtEndTimeValidating)
            AddHandler edtEndTime.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnEdtEndTimeInvalidValue)
            AddHandler cbReminder.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnCbReminderInvalidValue)
            AddHandler cbReminder.Validating, New CancelEventHandler(AddressOf OnCbReminderValidating)
        End Sub

        Protected Friend Overridable Sub UnsubscribeControlsEvents()
            RemoveHandler edtEndDate.Validating, New CancelEventHandler(AddressOf OnEdtEndDateValidating)
            RemoveHandler edtEndDate.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnEdtEndDateInvalidValue)
            RemoveHandler edtEndTime.Validating, New CancelEventHandler(AddressOf OnEdtEndTimeValidating)
            RemoveHandler edtEndTime.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnEdtEndTimeInvalidValue)
            RemoveHandler cbReminder.InvalidValue, New InvalidValueExceptionEventHandler(AddressOf OnCbReminderInvalidValue)
            RemoveHandler cbReminder.Validating, New CancelEventHandler(AddressOf OnCbReminderValidating)
        End Sub

        Private Sub OnBtnOkClick(ByVal sender As Object, ByVal e As EventArgs)
            OnOkButton()
        End Sub

        Protected Friend Overridable Sub OnEdtEndDateValidating(ByVal sender As Object, ByVal e As CancelEventArgs)
            e.Cancel = Not IsValidInterval()
            If Not e.Cancel Then edtEndDate.DataBindings("EditValue").WriteValue()
        End Sub

        Protected Friend Overridable Sub OnEdtEndDateInvalidValue(ByVal sender As Object, ByVal e As InvalidValueExceptionEventArgs)
            e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub

        Protected Friend Overridable Sub OnEdtEndTimeValidating(ByVal sender As Object, ByVal e As CancelEventArgs)
            e.Cancel = Not IsValidInterval()
            If Not e.Cancel Then edtEndTime.DataBindings("EditValue").WriteValue()
        End Sub

        Protected Friend Overridable Sub OnEdtEndTimeInvalidValue(ByVal sender As Object, ByVal e As InvalidValueExceptionEventArgs)
            e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate)
        End Sub

        Protected Friend Overridable Function IsValidInterval() As Boolean
            Return AppointmentFormControllerBase.ValidateInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay)
        End Function

        Protected Friend Overridable Sub OnOkButton()
            If Not SaveFormData(Controller.EditedAppointmentCopy) Then Return
            If Not Controller.IsConflictResolved() Then
                ShowMessageBox(SchedulerLocalizer.GetString(SchedulerStringId.Msg_Conflict), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            If Controller.IsAppointmentChanged() OrElse Controller.IsNewAppointment OrElse IsAppointmentChanged(Controller.EditedAppointmentCopy) Then Controller.ApplyChanges()
            DialogResult = DialogResult.OK
        End Sub

        Protected Friend Overridable Function ShowMessageBox(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
            Return XtraMessageBox.Show(Me, text, caption, buttons, icon)
        End Function

        Private Sub OnBtnDeleteClick(ByVal sender As Object, ByVal e As EventArgs)
            OnDeleteButton()
        End Sub

        Protected Friend Overridable Sub OnDeleteButton()
            If IsNewAppointment Then Return
            Controller.DeleteAppointment()
            DialogResult = DialogResult.Abort
            Close()
        End Sub

        Private Sub OnBtnRecurrenceClick(ByVal sender As Object, ByVal e As EventArgs)
            OnRecurrenceButton()
        End Sub

        Protected Friend Overridable Sub OnRecurrenceButton()
            If Not Controller.ShouldShowRecurrenceButton Then Return
            Dim patternCopy As Appointment = Controller.PrepareToRecurrenceEdit()
            Dim result As DialogResult
            Using form As Form = CreateAppointmentRecurrenceForm(patternCopy, Control.OptionsView.FirstDayOfWeek)
                result = ShowRecurrenceForm(form)
            End Using

            If result = DialogResult.Abort Then
                Controller.RemoveRecurrence()
            ElseIf result = DialogResult.OK Then
                Controller.ApplyRecurrence(patternCopy)
            End If
        End Sub

        Protected Overridable Function ShowRecurrenceForm(ByVal form As Form) As DialogResult
            Return FormTouchUIAdapter.ShowDialog(form, Me)
        End Function

        Protected Friend Overridable Function CreateAppointmentRecurrenceForm(ByVal patternCopy As Appointment, ByVal firstDayOfWeek As FirstDayOfWeek) As Form
            Dim form As AppointmentRecurrenceForm = New AppointmentRecurrenceForm(patternCopy, firstDayOfWeek, Controller)
            form.SetMenuManager(MenuManager)
            form.LookAndFeel.ParentLookAndFeel = LookAndFeel
            form.ShowExceptionsRemoveMsgBox = controllerField.AreExceptionsPresent()
            Return form
        End Function

        Friend Sub OnAppointmentFormActivated(ByVal sender As Object, ByVal e As EventArgs)
            If openRecurrenceFormField Then
                openRecurrenceFormField = False
                OnRecurrenceButton()
            End If
        End Sub

        Protected Friend Overridable Sub OnCbReminderValidating(ByVal sender As Object, ByVal e As CancelEventArgs)
            Dim span As TimeSpan = cbReminder.Duration
            e.Cancel = span = TimeSpan.MinValue OrElse span.Ticks < 0
            If Not e.Cancel Then cbReminder.DataBindings("Duration").WriteValue()
        End Sub

        Protected Friend Overridable Sub OnCbReminderInvalidValue(ByVal sender As Object, ByVal e As InvalidValueExceptionEventArgs)
            e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidReminderTimeBeforeStart)
        End Sub

        Protected Friend Overridable Sub RecalculateLayoutOfControlsAffectedByProgressPanel()
            If progressPanel.Visible Then Return
            Dim intDeltaY As Integer = progressPanel.Height
            tbDescription.Location = New Point(tbDescription.Location.X, tbDescription.Location.Y - intDeltaY)
            tbDescription.Size = New Size(tbDescription.Size.Width, tbDescription.Size.Height + intDeltaY)
        End Sub
    End Class
End Namespace
