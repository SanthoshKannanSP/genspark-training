import { Routes } from '@angular/router';
import { VideoList } from './video-list/video-list';
import { VideoDetail } from './video-detail/video-detail';
import { UploadVideo } from './upload-video/upload-video';

export const routes: Routes = [
  { path: '', redirectTo: 'videos', pathMatch: 'full' },
  { path: 'videos', component: VideoList },
  { path: 'videos/upload', component: UploadVideo },
  { path: 'videos/:fileName', component: VideoDetail },
];
