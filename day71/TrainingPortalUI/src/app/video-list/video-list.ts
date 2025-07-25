import { Component, OnInit } from '@angular/core';
import { Video } from '../models/video.model';
import { VideoService } from '../services/video.service';
import { Router, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-video-list',
  imports: [DatePipe, RouterLink],
  templateUrl: './video-list.html',
  styleUrl: './video-list.css',
})
export class VideoList implements OnInit {
  videos: Video[] = [];

  constructor(private videoService: VideoService, private router: Router) {}

  ngOnInit(): void {
    this.videoService.getAllVideos().subscribe((data) => (this.videos = data));
  }

  openVideo(video: Video): void {
    this.router.navigate(['/videos', video.videoId]);
  }
}
