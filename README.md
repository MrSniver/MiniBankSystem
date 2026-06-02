# Mini System Bankowy

## Opis projektu

Mini System Bankowy to aplikacja konsolowa umożliwiająca podstawową obsługę kont bankowych. Projekt został podzielony na logiczne moduły odpowiedzialne za konkretne obszary działania systemu

---

## Dane logowania i konta testowe

| Login | Hasło | Numer Konta Bankowego |
|-----------|-----------|-----------|
| JohnDoe15    | JDoe25    | PL09109010140000000123456789    |
| JaneD90    | D9Jane    | PL45916841290740172176509724    |

---

## Struktura projektu

Projekt składa się z następujących folderów:

### Entities
Zawiera modele danych wykorzystywane w aplikacji, takie jak:

- `Account` – model konta bankowego
- `AccountOwner` – model właściciela konta

### InputControl
Folder zawiera klasy odpowiedzialne za weryfikację danych wejściowych użytkownika z wykorzystaniem wyrażeń regularnych (Regex).

### PrintControl
Zawiera klasę ułatwiającą wyświetlanie elementów interfejsu konsolowego, takich jak:

- nagłówki aplikacji
- menu użytkownika

### AccountActions
Moduł odpowiedzialny za logikę biznesową aplikacji.

#### Interfaces
Zawiera interfejsy dla klas:

- `AccountManagementActions`
- `TransactionActions`

#### Implementacje
Klasy implementujące logikę biznesową dotyczącą:

- zarządzania kontami
- wykonywania operacji finansowych
- obsługi transakcji

### Services
Zawiera:

- interfejs serwisowy
- klasę `AccountService`

Moduł odpowiada za:

- dostęp do danych kont bankowych
- manipulację danymi w aplikacji

### Validators
Folder zawiera walidatory sprawdzające poprawność danych wprowadzanych przez użytkownika.

---

## Zastosowane wzorce projektowe

W projekcie wykorzystano między innymi:

- `Validator Pattern` – odpowiedzialny za walidację danych wejściowych użytkownika

---

## System logowania

Aplikacja posiada podstawowy system logowania umożliwiający:

- korzystanie z wcześniej utworzonych kont
- stworzenie nowego konta


