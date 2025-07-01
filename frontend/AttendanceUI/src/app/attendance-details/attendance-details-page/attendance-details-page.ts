import { Component } from '@angular/core';
import { FilterMenu } from '../../my-sessions/filter-menu/filter-menu';
import { AttendanceTable } from '../attendance-table/attendance-table';

@Component({
  selector: 'app-attendance-details-page',
  imports: [FilterMenu, AttendanceTable],
  templateUrl: './attendance-details-page.html',
  styleUrl: './attendance-details-page.css',
})
export class AttendanceDetailsPage {}
