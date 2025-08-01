export class NewsModel {
  newsId: number = 0;
  title: string = '';
  shortDescription: string = '';
  image: string = '';
  content: string = '';
  createdDate!: Date;
  status: number = 0;
  userId: number = 0;
  username: string = '';
}
