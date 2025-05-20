class EmployeePromotion
{
    private List<string> promotionList = new List<string>();

    public EmployeePromotion() {}

    public void ClearPromotionList()
    {
        promotionList.Clear();
    }

    public void CreatePromotionList(EmployeeDirectory employeeList)
    {
        Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop)");
        var employeeName = Console.ReadLine().Trim();
        while (employeeName.Any())
        {
            if (employeeList.HasEmployee(employeeName))
                promotionList.Add(employeeName);
            else
                Console.WriteLine($"Employee with name {employeeName} doesn't exist!");
            employeeName = Console.ReadLine().Trim();
        }
    }

    public void FindPromotionPosition(string employeeName)
    {
        var position = promotionList.IndexOf(employeeName);
        if (position == -1)
        {
            Console.WriteLine($"Employee with name {employeeName} is not found in promotion list!");
            return;
        }
        Console.WriteLine($"\"{employeeName}\" is in the position {position + 1} for promotion");
    }

    public int GetCurrentMemorySize()
    {
        return promotionList.Capacity;
    }

    public void FreeExcessMemory()
    {
        promotionList.TrimExcess();
    }

    public void PromoteEmployees()
    {
        if (promotionList.Count == 0)
        {
            Console.WriteLine("No employees to promote!");
            return;
        }
        promotionList.Sort();
        foreach(var employee in promotionList)
            Console.WriteLine(employee);
        ClearPromotionList();
    }
}