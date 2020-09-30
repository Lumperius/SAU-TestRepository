export class News{
  id: string;
  article: string;
  plainText: string;
  datePosted: string;
  source: string;
  rating: number;

  constructor(newsResponse: any) {
    this.id = newsResponse.id;
    this.article = newsResponse.article;
    this.plainText = newsResponse.plainText;
    this.source = newsResponse.source;
    this.rating = newsResponse.rating;
    const today = new Date();
    this.datePosted = today.getHours() + ':'
    + today.getMinutes() + ' ' + today.getDate()
    + '.' + (today.getMonth() + 1);
  }
}
