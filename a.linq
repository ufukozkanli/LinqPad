<Query Kind="Program">
  <Connection>
    <ID>71cffb82-448c-4d1c-8ebc-666c3c2050be</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Users\ufuko\Source\Repos\tigem\Tigem.Business\bin\Debug\Tigem.Business.dll</CustomAssemblyPath>
    <CustomTypeName>Tigem.Business.Data.Model.TigemDBEntities</CustomTypeName>
    <AppConfigPath>C:\Users\ufuko\Source\Repos\tigem\Tigem.Business\bin\Debug\Tigem.Business.dll.config</AppConfigPath>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>Tigem.Tools</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

public class Tem { 
	public string Name  { get; set; }
	public string PropertyName{ get; set; }
}

void Main()
{
	
	var list=typeof(Tigem.Business.Data.Model.TigemDBEntities).GetProperties();
	//.Dump();
	var ty = list.Select(o => o.PropertyType.GenericTypeArguments.FirstOrDefault()).Where(o => o != null);
	//.Dump();
	 
	var dictionary=new Dictionary<string,List<Tem>>(); 
	foreach (var e in ty)
	{	
		dictionary[e.Name]= e.GetProperties().Where(t=> t.PropertyType.IsPrimitive|| t.PropertyType == typeof(Decimal) || t.PropertyType == typeof(String) ||t.PropertyType==typeof(DateTime) ).Select(o => new Tem {Name=o.Name, PropertyName=o.PropertyType.Name}).ToList();
	}
	
	dictionary.Keys.OrderBy(o=>o).Dump();
	var str = GenerateOutoCode(dictionary);
}
public string GenerateOutoCode(Dictionary<string, List<Tem>> dic)
{	
	var map = new Dictionary<string, string> {
	{"Int32", "Integer"},
	{"Decimal", "Real"},
	{"String", "String"},
	{"DateTime", "DateTime"},
	{"Boolean", "Enum"},
	{"Byte", "Integer"},
	{"Double", "Real"},
	};
	var tpl = @"
TableName='_{0}';
treeNode.AOTadd(TableName);
fieldlist = TreeNode::findNode('\\Data Dictionary\\Tables\\' + TableName + '\\Fields');
{1}";
	var tpl2 = @"fieldList.add{0}('{1}');"+Environment.NewLine;
	var str = new StringBuilder();
	var str2 = new StringBuilder();
	var t = GetList();
	foreach (var e in dic.Keys.Where(o => !t.Contains(o)).OrderBy(o=>o))
	{
		str2.Clear();
		foreach (var d in dic[e])
		{
			str2.AppendFormat(tpl2, map[d.PropertyName], d.Name);
		}
		str.AppendFormat(tpl, e, str2).Dump();

	}
	return str.ToString();
}
public static List<string> GetList()
{
	var t = @"BudgetPrice 
EquipmentExpenseGroup 
InputOutputDocument 
InputOutputDocumentSignature 
InputOutputEquipment 
LiveAsset 
NoteOfExpense 
SetOfUnit 
StockKind 
StockPeriod 
EquipmentStore 
EquipmentStoreShelf 
EquipmentType 
EquipmentTypeAlternative 
EquipmentTypeCountingError 
EquipmentTypeCountingErrorDetail 
EquipmentTypeGroup 
EquipmentTypeRequest 
EquipmentTypeSpecialCode 
EquipmentTypeUnit
Unit 
UnitSetOfUnit 
FA_AbandonManagement 
FA_AccountPlan 
FA_BankIntegration 
FA_CheckBill 
FA_DeptReceivable 
FA_DeptReceivablePayment 
FA_Embezzlement 
FA_EmbezzlementEquipment 
FA_Expiration 
FA_ExpirationPayment 
FA_IncomeExpense 
FA_InstutionBankAccount 
FA_Invoice 
FA_PaymentDocument 
FA_PaymentInstruction 
FA_Receipt 
FA_ReversalRecord 
HR_SalaryCalculationFactor 
HR_ShiftSchedule 
HR_SocialAid 
HR_SocialAidDetail 
HR_Worker 
Business 
BusinessDepartment 
Shelf 
Store 
";
	return t.Split(new[] { "\n"},StringSplitOptions.RemoveEmptyEntries).Select(o=>o.Trim()).ToList();
}