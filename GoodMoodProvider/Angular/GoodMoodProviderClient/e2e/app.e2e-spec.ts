import { GoodMoodProviderClientPage } from './app.po';

describe('good-mood-provider-client App', function() {
  let page: GoodMoodProviderClientPage;

  beforeEach(() => {
    page = new GoodMoodProviderClientPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
