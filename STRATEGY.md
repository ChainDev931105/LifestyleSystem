# Strategy Interface

## *_plot_indicator*

```C#
_plot_indicator(id, value)
```

- Draw [id]th indicator to chart

|param|meaning|example|
|-|-|-|
|id|index number of indicator|1, 2, 3, 4|
|value|price to plot to chart|(any number)|

## *_plot_pnt*

```C#
_plot_pnt(key, value, comment, timeframe)
```

- Plot point to chart

|param|meaning|example|
|-|-|-|
|key|indexing key of point|ex. "A2"|
|value|price to plot to chart|(any number)|
|comment|description about pnt|(any note string, empty string is also possible)|
|timeframe|to avoid multiple plotting|(valid timeframe, ex. M1, M4, ...)|

## *_lots*

```C#
_lots()
```

- returns total amount of current opened positions. negative if totally SELL opened.

## *_time*

```C#
_time()
```

- returns current time

|property|example|
|-|-|
|_time().Second|1 ~ 31|
|_time().Minute|1 ~ 31|
|_time().Hour|1 ~ 31|
|_time().Day|1 ~ 31|
|_time().Month|1 ~ 12|
|_time().Year|ex. 2021|
|_time().DayOfWeek|ex. DayOfWeek.Monday|

## *_order*

```C#
_order(lots, direction)
```

- set order

|param|meaning|example|
|-|-|-|
|lots|amount to set order|ex. 1|
|direction|direction|"BUY", "SELL", "BUYCLOSE", "SELLCLOSE"|

## *_value*

```C#
_value(a, b, c, d)
```

- getting value. just same as in orginal mode

## *_set*

```C#
_set(timeframe, pattern)
```

- set pattern

|param|meaning|example|
|-|-|-|
|timeframe|timeframe to set|ex. "M5"|
|pattern|pattern to set|ex. "a2B1"|
