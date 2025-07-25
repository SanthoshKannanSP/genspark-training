import { Component, OnInit } from '@angular/core';
import { Video } from '../models/video.model';
import { ActivatedRoute } from '@angular/router';
import { VideoService } from '../services/video.service';
import { DatePipe } from '@angular/common';
import { VideoPlayerComponent } from '../video-player/video-player';

@Component({
  selector: 'app-video-detail',
  imports: [DatePipe, VideoPlayerComponent],
  templateUrl: './video-detail.html',
  styleUrl: './video-detail.css',
})
export class VideoDetail implements OnInit {
  video!: Video;
  fileName: string = '';
  streamUrl: string = '';

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService
  ) {}

  ngOnInit(): void {
    this.fileName = this.route.snapshot.paramMap.get('fileName')!;
    this.videoService.getVideoDetails(this.fileName).subscribe((data) => {
      this.video = data;
      this.streamUrl = this.videoService.getVideoStreamUrl(this.fileName);
    });
  }
}
