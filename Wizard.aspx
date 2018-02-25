<form runat="server">
<asp:Wizard runat="server" id="WizardBook" style="border:solid 1px" width="300" height="100" onfinishbuttonclick="Finished">
 <wizardSteps>
  <asp:WizardStep steptype="Start">Thanks for choosing our site.</asp:WizardStep>
  <asp:WizardStep steptype="Step">Please choose the method of payment.</asp:WizardStep>
  <asp:WizardStep steptype="Finish">Thanks for shopping with us.</asp:WizardStep>
  <asp:WizardStep steptype="Complete"><asp:label runat="server" id="FinalMessage" /></asp:WizardStep>
 </wizardSteps>
</asp:Wizard>
</form>

<script runat="server" language="C#">
 void Finished(object sender, EventArgs e)
 {
  FinalMessage.Text = "See you, soon";
 }
</script>