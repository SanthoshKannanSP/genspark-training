import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MySessionsPage } from './my-sessions-page';

describe('MySessionsPage', () => {
  let component: MySessionsPage;
  let fixture: ComponentFixture<MySessionsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MySessionsPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MySessionsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
