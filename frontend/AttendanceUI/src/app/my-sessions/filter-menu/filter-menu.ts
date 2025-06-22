import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-menu',
  imports: [ReactiveFormsModule],
  templateUrl: './filter-menu.html',
  styleUrl: './filter-menu.css',
})
export class FilterMenu {
  filtersChanged = new EventEmitter<any>();

  filterForm: FormGroup;

  statuses = ['Scheduled', 'Cancelled', 'Completed', 'Live'];

  constructor(private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      title: [''],
      status: [''],
      startDate: [''],
      endDate: [''],
      startTime: [''],
      endTime: [''],
    });

    this.filterForm.valueChanges.subscribe((value) => {
      this.filtersChanged.emit(value);
    });
  }

  applyFilters() {
    this.filtersChanged.emit(this.filterForm.value);
  }

  resetFilters() {
    this.filterForm.reset();
  }
}
