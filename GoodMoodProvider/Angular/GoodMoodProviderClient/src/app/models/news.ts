export class News{
  id: string;
  article: string;
  body: string;
  source: string;
  rating: number;

  constructor(newsResponse: any) {
    this.id = newsResponse.id;
    this.article = newsResponse.article;
    this.body = newsResponse.body;
    this.source = newsResponse.source;
    this.rating = newsResponse.rating;
    const today = new Date();
  }
}
