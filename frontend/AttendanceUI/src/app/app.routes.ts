import { Routes } from '@angular/router';
import { HeroPage } from './hero-page/hero-page';
import { LoginPage } from './login-page/login-page';
import { SignUpChoosePage } from './sign-up-choose-page/sign-up-choose-page';
import { DashboardPage } from './dashboard/dashboard-page/dashboard-page';
import { PortalPage } from './portal-page/portal-page';
import { MySessionsPage } from './my-sessions/my-sessions-page/my-sessions-page';
import { AttendanceDetailsPage } from './attendance-details/attendance-details-page/attendance-details-page';
import { SettingsPage } from './settings-page/settings-page';
import { studentAuthGuard } from './student-auth-guard';
import { teacherAuthGuard } from './teacher-auth-guard';
import { UnauthorizedPage } from './unauthorized-page/unauthorized-page';
import { anyAuthGuard } from './any-auth-guard';
import { TeacherSignupPage } from './teacher-signup-page/teacher-signup-page';
import { StudentSignupPage } from './student-signup-page/student-signup-page';
import { InvitePage } from './invite-page/invite-page';
import { LiveSessionControl } from './live-session-control/live-session-control';

export const routes: Routes = [
  { path: '', component: HeroPage },
  { path: 'unauthorized', component: UnauthorizedPage },
  { path: 'login', component: LoginPage },
  { path: 'signup', component: SignUpChoosePage },
  { path: 'signup/teacher', component: TeacherSignupPage },
  { path: 'signup/student', component: StudentSignupPage },
  { path: 'invite/:sessionCode', component: InvitePage },
  { path: 'session/live', component: LiveSessionControl },
  {
    path: 'portal',
    component: PortalPage,
    children: [
      {
        path: 'dashboard',
        component: DashboardPage,
        canActivate: [anyAuthGuard],
      },
      {
        path: 'sessions',
        component: MySessionsPage,
        canActivate: [anyAuthGuard],
      },
      {
        path: 'attendance',
        component: AttendanceDetailsPage,
        canActivate: [anyAuthGuard],
      },
      {
        path: 'settings',
        component: SettingsPage,
        canActivate: [anyAuthGuard],
      },
    ],
  },
];
