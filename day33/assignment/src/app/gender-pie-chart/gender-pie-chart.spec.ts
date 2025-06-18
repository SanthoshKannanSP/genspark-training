import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenderPieChart } from './gender-pie-chart';

describe('GenderPieChart', () => {
  let component: GenderPieChart;
  let fixture: ComponentFixture<GenderPieChart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenderPieChart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenderPieChart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
