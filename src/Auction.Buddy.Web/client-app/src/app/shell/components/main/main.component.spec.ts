import { By } from '@angular/platform-browser';
import { renderWithStore } from '../../../../testing';
import { ShellActions } from '../../actions';
import { MainComponent } from './main.component';

describe('MainComponent', () => {
  it('should trigger toggle sidenav', async () => {
    const { store, getByTestId, click } = await renderWithStore(MainComponent);

    click(getByTestId('toggle-sidenav'));

    expect(store.getActions()).toContain(ShellActions.toggleSidenav());
  });

  it('should have open sidenav', async () => {
    const { getByTestId } = await renderWithStore(MainComponent, ShellActions.toggleSidenav());

    expect(getByTestId('sidenav').style['visibility']).toEqual('visible');
  });

  it('should trigger close sidenav', async () => {
    const { store, fixture } = await renderWithStore(MainComponent);

    fixture.debugElement.query(By.css('[data-testId="sidenav"]'))
      .triggerEventHandler('closed', null);

    expect(store.getActions()).toContain(ShellActions.closeSidenav());
  })
});
