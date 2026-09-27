import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <a class="skip-link" href="#main-content">Skip to main content</a>

    <header class="site-header">
      <div class="container nav-wrap">
        <a class="brand" routerLink="/" aria-label="Probesh home">
          <span class="brand__mark" aria-hidden="true">প</span>
          <span>
            <strong>Probesh</strong>
            <small>প্রবেশ · Access for everyone</small>
          </span>
        </a>

        <nav aria-label="Primary navigation">
          <a routerLink="/" [routerLinkActiveOptions]="{ exact: true }" routerLinkActive="active">
            Explore
          </a>
          <a routerLink="/places/new" routerLinkActive="active">Add a place</a>
        </nav>
      </div>
    </header>

    <main id="main-content">
      <router-outlet />
    </main>

    <footer>
      <div class="container footer-wrap">
        <p><strong>Probesh</strong> is a learning MVP. Always confirm accessibility with the venue.</p>
        <p>Built in Bangladesh with inclusion in mind.</p>
      </div>
    </footer>
  `,
  styles: `
    .skip-link {
      position: fixed;
      z-index: 100;
      top: 0.75rem;
      left: 0.75rem;
      padding: 0.65rem 0.9rem;
      border-radius: 0.5rem;
      background: var(--ink);
      color: #fff;
      transform: translateY(-160%);
    }

    .skip-link:focus {
      transform: translateY(0);
    }

    .site-header {
      position: sticky;
      z-index: 10;
      top: 0;
      border-bottom: 1px solid rgba(215, 226, 222, 0.9);
      background: rgba(255, 255, 255, 0.94);
      backdrop-filter: blur(12px);
    }

    .nav-wrap,
    .footer-wrap {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 1.5rem;
    }

    .nav-wrap {
      min-height: 74px;
    }

    .brand {
      display: inline-flex;
      align-items: center;
      gap: 0.7rem;
      color: var(--ink);
      text-decoration: none;
    }

    .brand__mark {
      display: grid;
      width: 42px;
      height: 42px;
      place-items: center;
      border-radius: 0.8rem 0.8rem 0.8rem 0.2rem;
      background: var(--brand);
      color: white;
      font-size: 1.4rem;
      font-weight: 850;
    }

    .brand strong,
    .brand small {
      display: block;
    }

    .brand small {
      margin-top: 0.08rem;
      color: var(--muted);
      font-size: 0.72rem;
    }

    nav {
      display: flex;
      align-items: center;
      gap: 0.35rem;
    }

    nav a {
      padding: 0.62rem 0.78rem;
      border-radius: 0.55rem;
      color: var(--muted);
      font-weight: 700;
      text-decoration: none;
    }

    nav a:hover,
    nav a.active {
      background: #e8f2ef;
      color: var(--brand-dark);
    }

    footer {
      margin-top: 5rem;
      border-top: 1px solid var(--line);
      background: var(--surface);
    }

    .footer-wrap {
      min-height: 100px;
      color: var(--muted);
      font-size: 0.88rem;
    }

    @media (max-width: 620px) {
      .nav-wrap {
        align-items: flex-start;
        flex-direction: column;
        padding-block: 0.8rem;
      }

      nav {
        width: 100%;
      }

      nav a {
        flex: 1;
        text-align: center;
      }

      .footer-wrap {
        align-items: flex-start;
        flex-direction: column;
        justify-content: center;
        padding-block: 1.5rem;
      }

      .footer-wrap p {
        margin: 0;
      }
    }
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {}
