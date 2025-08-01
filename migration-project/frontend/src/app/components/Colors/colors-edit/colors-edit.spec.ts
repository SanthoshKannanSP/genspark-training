import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorsEdit } from './colors-edit';

describe('ColorsEdit', () => {
  let component: ColorsEdit;
  let fixture: ComponentFixture<ColorsEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ColorsEdit]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColorsEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
