import { Component } from '@angular/core';
import { UserTable } from '../user-table/user-table';
import { FilterMenu } from '../filter-menu/filter-menu';

@Component({
  selector: 'app-filter-page',
  imports: [UserTable, FilterMenu],
  templateUrl: './filter-page.html',
  styleUrl: './filter-page.css',
})
export class FilterPage {}
