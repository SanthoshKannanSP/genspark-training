import { Routes } from '@angular/router';
import { HeroPage } from './hero-page/hero-page';
import { LoginPage } from './login-page/login-page';
import { DashboardPage } from './dashboard/dashboard-page/dashboard-page';
import { PortalPage } from './portal-page/portal-page';
import { MySessionsPage } from './my-sessions/my-sessions-page/my-sessions-page';
import { AttendanceDetailsPage } from './attendance-details/attendance-details-page/attendance-details-page';
import { studentAuthGuard } from './student-auth-guard';
import { teacherAuthGuard } from './teacher-auth-guard';
import { adminAuthGuard } from './admin-auth-guard';
import { UnauthorizedPage } from './unauthorized-page/unauthorized-page';
import { anyAuthGuard } from './any-auth-guard';
import { TeacherSignupPage } from './teacher-signup-page/teacher-signup-page';
import { StudentSignupPage } from './student-signup-page/student-signup-page';
import { InvitePage } from './invite-page/invite-page';
import { LiveSessionControl } from './live-session-control/live-session-control';
import { SettingsPage } from './settings/settings-page/settings-page';
import { AdminDashboard } from './admin-dasboard/admin-dashboard/admin-dashboard';
import { AttendanceEditRequests } from './admin-dasboard/attendance-edit-requests/attendance-edit-requests';
import { ManageStudents } from './admin-dasboard/manage-students/manage-students';
import { ManageTeachers } from './admin-dasboard/manage-teachers/manage-teachers';

export const routes: Routes = [
  { path: '', component: HeroPage },
  { path: 'unauthorized', component: UnauthorizedPage },
  { path: 'login', component: LoginPage },
  { path: 'signup/teacher', component: TeacherSignupPage },
  { path: 'signup/student', component: StudentSignupPage },
  {
    path: 'invite/:sessionCode',
    component: InvitePage,
    canActivate: [studentAuthGuard],
  },
  {
    path: 'session/live',
    component: LiveSessionControl,
    canActivate: [teacherAuthGuard],
  },
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
        canActivate: [teacherAuthGuard],
      },
      {
        path:'admin-dashboard',
        component: AdminDashboard,
        canActivate: [adminAuthGuard],
        children:[
          {
            path: 'attendance-edit-requests',
            component: AttendanceEditRequests
          },
          {
            path: 'manage-students',
            component: ManageStudents
          },
          {
            path: 'manage-teachers',
            component: ManageTeachers
          }
        ]
      },
      {
        path: 'settings',
        component: SettingsPage,
        canActivate: [anyAuthGuard],
      },
    ],
  },
];
