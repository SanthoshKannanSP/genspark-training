import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorsDetails } from './colors-details';

describe('ColorsDetails', () => {
  let component: ColorsDetails;
  let fixture: ComponentFixture<ColorsDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ColorsDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColorsDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
