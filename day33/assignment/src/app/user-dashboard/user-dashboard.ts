import { Component, inject } from '@angular/core';
import { UserService } from '../services/user-service';
import { UserModel } from '../models/user-model';
import { GenderPieChart } from '../gender-pie-chart/gender-pie-chart';
import { RoleBarChart } from '../role-bar-chart/role-bar-chart';
import { UserTable } from '../user-table/user-table';
import { FilterMenu } from '../filter-menu/filter-menu';

@Component({
  selector: 'app-user-dashboard',
  imports: [GenderPieChart, RoleBarChart, UserTable, FilterMenu],
  templateUrl: './user-dashboard.html',
  styleUrl: './user-dashboard.css',
})
export class UserDashboard {
  userService = inject(UserService);
}
