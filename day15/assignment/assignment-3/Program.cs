using assignment_3.Models;
using assignment_3.Service;

string data = File.ReadAllText("data.csv");
DataFrameAdapter csvData = new DataFrameAdapter(data);
AnalyticsService analytics = new AnalyticsService();
analytics.FindMean(csvData);