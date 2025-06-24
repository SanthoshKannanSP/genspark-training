import { Routes } from '@angular/router';
import { HeroPage } from './hero-page/hero-page';
import { LoginPage } from './login-page/login-page';
import { SignUpChoosePage } from './sign-up-choose-page/sign-up-choose-page';
import { DashboardPage } from './dashboard/dashboard-page/dashboard-page';
import { PortalPage } from './portal-page/portal-page';
import { MySessionsPage } from './my-sessions/my-sessions-page/my-sessions-page';
import { ScheduleSessionPage } from './my-sessions/schedule-session-page/schedule-session-page';

export const routes: Routes = [
  { path: '', component: HeroPage },
  { path: 'login', component: LoginPage },
  { path: 'sign-up', component: SignUpChoosePage },
  {
    path: 'portal',
    component: PortalPage,
    children: [
      { path: 'dashboard', component: DashboardPage },
      { path: 'sessions', component: MySessionsPage },
      { path: 'sessions/new', component: ScheduleSessionPage },
    ],
  },
];
