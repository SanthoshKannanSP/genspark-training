import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleBarChart } from './role-bar-chart';

describe('RoleBarChart', () => {
  let component: RoleBarChart;
  let fixture: ComponentFixture<RoleBarChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleBarChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleBarChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
