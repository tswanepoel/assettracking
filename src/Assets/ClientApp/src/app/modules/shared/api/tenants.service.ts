import { Injectable } from '@angular/core';

import { AuthService } from '../auth.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TenantsService {

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient
  ) { }

  async getTenants(): Promise<any> {
    const client = await this.authService.getAuth0Client();
    const token = await client.getTokenSilently();

    return this.httpClient
      .get('/api/tenants', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      .toPromise();
  }
}
