import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsManagementDetails } from './news-management-details';

describe('NewsManagementDetails', () => {
  let component: NewsManagementDetails;
  let fixture: ComponentFixture<NewsManagementDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewsManagementDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsManagementDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
