import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PastSessionCard } from './past-session-card';

describe('PastSessionCard', () => {
  let component: PastSessionCard;
  let fixture: ComponentFixture<PastSessionCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PastSessionCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PastSessionCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
