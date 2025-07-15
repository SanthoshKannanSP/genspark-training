import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MySessionsPage } from './my-sessions-page';
import { HttpClientService } from '../../services/http-client-service';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';

@Component({
  selector: 'app-filter-menu',
  template: '',
})
class MockFilterMenu {}

@Component({
  selector: 'app-sessions-table',
  template: '',
})
class MockSessionsTable {}

@Component({
  selector: 'app-schedule-session-button',
  template: '',
})
class MockScheduleSessionButton {}

describe('MySessionsPage', () => {
  let component: MySessionsPage;
  let fixture: ComponentFixture<MySessionsPage>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['hasRole']);
    await TestBed.configureTestingModule({
      imports: [MySessionsPage],
      providers: [{ provide: HttpClientService, useValue: apiSpy }],
    })
      .overrideComponent(MySessionsPage, {
        set: {
          imports: [
            MockFilterMenu,
            MockSessionsTable,
            MockScheduleSessionButton,
          ],
        },
      })
      .compileComponents();

    fixture = TestBed.createComponent(MySessionsPage);
    component = fixture.componentInstance;

    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render schedule session button if teacher', () => {
    mockHttpClientService.hasRole.and.returnValue(true);
    fixture.detectChanges();
    const scheduleButton = fixture.debugElement.query(By.css('button'));

    expect(scheduleButton).toBeTruthy();
  });

  it('should not render schedule session button if student', () => {
    mockHttpClientService.hasRole.and.returnValue(false);
    fixture.detectChanges();
    const scheduleButton = fixture.debugElement.query(By.css('button'));

    expect(scheduleButton).toBeFalsy();
  });
});
