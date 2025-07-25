import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.html',
  styleUrls: ['./video-player.css'],
})
export class VideoPlayerComponent implements AfterViewInit {
  @ViewChild('video', { static: true }) videoRef!: ElementRef<HTMLVideoElement>;
  @Input() fileName: string = '';

  ngAfterViewInit() {
    const videoElement = this.videoRef.nativeElement;
    const mediaSource = new MediaSource();
    videoElement.src = URL.createObjectURL(mediaSource);

    mediaSource.addEventListener('sourceopen', () => {
      const mime = 'video/mp4; codecs="avc1.64001E, mp4a.40.2"';
      const sourceBuffer = mediaSource.addSourceBuffer(mime);

      fetch(`http://localhost:5083/api/Video/${this.fileName}/stream`).then(
        (res) => {
          const reader = res.body?.getReader();
          const pump = () => {
            reader?.read().then(({ done, value }) => {
              if (done) {
                mediaSource.endOfStream();
                return;
              }
              sourceBuffer.appendBuffer(value);
              if (!sourceBuffer.updating) {
                pump();
              } else {
                sourceBuffer.addEventListener('updateend', pump, {
                  once: true,
                });
              }
            });
          };
          pump();
        }
      );
    });
  }
}
