import { Component, OnInit } from '@angular/core';
import Auth0Client from '@auth0/auth0-spa-js/dist/typings/Auth0Client';
import { faGlobe, faBars, faCog, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../shared/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  title = 'Asset Tracking';
  appIcon = faGlobe;
  menuIcon = faBars;
  settingsIcon = faCog;
  signOutIcon = faSignOutAlt;
  isCollapsed = true;

  isAuthenticated = false;
  profile: any;

  private auth0Client: Auth0Client;

  constructor(private authService: AuthService) {}

  async ngOnInit() {
    // Get an instance of the Auth0 client
    this.auth0Client = await this.authService.getAuth0Client();

    // Watch for changes to the isAuthenticated state
    this.authService.isAuthenticated.subscribe(value => {
      this.isAuthenticated = value;
    });

    // Watch for changes to the profile data
    this.authService.profile.subscribe(profile => {
      this.profile = profile;
    });
  }

  /**
   * Logs in the user by redirecting to Auth0 for authentication
   */
  async signIn() {
    await this.auth0Client.loginWithRedirect({ redirect_uri: `${window.location.origin}/callback` });
  }

  /**
   * Logs the user out of the applicaion, as well as on Auth0
   */
  signOut() {
    this.auth0Client.logout({
      client_id: this.authService.config.client_id,
      returnTo: window.location.origin
    });
  }
}
