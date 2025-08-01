import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorsCreate } from './colors-create';

describe('ColorsCreate', () => {
  let component: ColorsCreate;
  let fixture: ComponentFixture<ColorsCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ColorsCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColorsCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
