# Finansowy Menadżer Budżetu

**Finansowy Menadżer Budżetu** to aplikacja umożliwiająca zarządzanie finansami poprzez dodawanie transakcji, przypisywanie ich do kategorii, grup, oraz załączanie plików. Program umożliwia także śledzenie statystyk użytkownika.

## Funkcje

### 1. Zarządzanie Transakcjami

- Dodawanie nowych transakcji, zawierających informacje takie jak data, kategoria, grupa, kwota, komentarz, tytuł, oraz plik.
- Przypisywanie transakcji do kategorii i grup.
- Załączanie plików, takich jak rachunki, do każdej transakcji.

### 2. Zarządzanie Kategoriami i Grupami

- Dodawanie nowych kategorii i grup, umożliwiających lepszą organizację transakcji.
- Kategorię może dodać tylko administrator.

### 3. Statystyki Użytkownika

- Wyświetlanie ogólnych statystyk użytkownika, takich jak łączna ilość transakcji, łączna kwota, bilans (przychody - wydatki), ilość wgranych plików.
- Filtrowanie statystyk według określonego zakresu dat.

### 4. Statystyki Serwisu

- Dostęp do ogólnych statystyk serwisu, obejmujących wszystkich użytkowników.

## Instrukcje Użytkownika

1. **Dodawanie Transakcji:**
   - Przejdź do opcji "Dodaj Transakcję" w sekcji Transakcje.
   - Wprowadź wszystkie wymagane informacje, a następnie kliknij "Dodaj".

2. **Zarządzanie Grupami:**
   - Przejdź do odpowiednich sekcji w menu.
   - Dodawaj nowe grupy.

3. **Przeglądanie Statystyk Użytkownika:**
   - Przejdź do sekcji "Statystyki".
   - Sprawdzaj ogólne statystyki oraz dostosowuj zakres dat.

4. **Przeglądanie Statystyk Serwisu:**
   - Przejdź do sekcji "Home".
   - Sprawdzaj ogólne statystyki obejmujące wszystkich użytkowników.

## Wymagania Systemowe

- .NET Core 6.0 lub nowszy
- SQL Server dla przechowywania danych

## Instalacja i Uruchamianie

1. Sklonuj repozytorium.
2. Ustaw połączenie z bazą danych w pliku `appsettings.json`.
3. Ustaw adres e-mail administratora w pliku `Program.cs`.
4. Uruchom komendę `dotnet run` w terminalu w katalogu projektu.

## Autor

*Kamil Priefer*


