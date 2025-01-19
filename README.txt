SocialMediaApp to aplikacja społecznościowa, która umożliwia użytkownikom:

- Rejestrację i logowanie.
- Tworzenie, przeglądanie, edytowanie i usuwanie postów.
- Interakcję z postami za pomocą reakcji, takich jak polubienia, śmiechy itp.
Aplikacja została zbudowana z wykorzystaniem React na front-endzie oraz ASP.NET Core z Entity Framework na back-endzie. Wszystkie dane są przechowywane w relacyjnej bazie danych SQL. Autentykacja i autoryzacja wykonana jest przy użyciu tokenów JWT.

Funkcje:

- Dla niezalogowanych użytkowników:
Rejestracja.
Logowanie.
- Dla zalogowanych użytkowników:
Przeglądanie listy postów.
Dodawanie nowych postów.
Edycja swoich postów.
Usuwanie swoich postów.
Reagowanie na posty (np. polubienia, śmiechy).

Instalacja i konfiguracja:

Aby włączyć wszystkie serwisy (IdentityApi, PostApi, ReactionsApi, frontend oraz bazę danych w SqlServer) to:
- przechodzimy do katalogu root, gdzie znajduje się plik docker-compose.yml
- otwieramy terminal i wpisujemy: docker compose up --build (zbudują się wszystkie obrazy i stworzą kontenery)
- baza danych powinna być automatycznie zapełniona przykładowymi danymi
- wchodzimy na http://localhost:3000/ i aplikacja powinna działać
- przykładowo tworzymy nowe konto lub logujemy się na istniejące np. username: tomekkowalski, password: Password123

Jeżeli będą jakieś problemy to spróbować usunąć bazę danych, usunąć kontenery i odpalić raz jeszcze: docker compose up --build

Jeżeli chcemy zobaczyć co jest w dockerowej bazie danych to:
- wchodzimy do SSMS
- łączymy się z serwerem używając tych parametrów:

Server name: localhost, 1433
Authentication: SQL Server Authentication
Login: sa
Password: YourStrongPassword!