# blazor-autocomplete
This project contains two simple controls, that I could not find outside of large, heavy frameworks.
Unfortunately I am not an advanced user of Blazor and CSS styling, so there might be some issues or bugs. Feel free to fork and improve the code. 

## Autocomplete input 
This is a simple text input control based on build-in Blazor `InputText`. The control displays list of matching ('Contains' method is used) elements from linked collection.

| Parameter     | Type | Default value | Descrption  |
| ------------- | -----|:-------------:| ------------|
| Source      | `List<string>` | empty | List of autocomplete items |
| Placeholder | `string` | `null` | Placeholder |
| MinTextLength | `int` | `2` | Minimum text length to open autcomplete popup |

### Example
To use the control, simply place it inside `EditForm`:
```csharp
<EditForm Model="@Model">
    <div class="form-group">
        <Autocomplete Source="@countries" @bind-Value="@Model.CountryName" class="form-control"/>
    </div>
</EditForm>
```
The minimal code should be like:
```csharp
@code {
    private List<string> countries = new List<string> { /*some data*/ }
    private AutocompleteModel Model = new AutocompleteModel();
}
```
Where `AutocompleteModel` is a simple POCO class with some `string` `CountryName` property:
```csharp
public class AutocompleteModel
{
    public string CountryName { get; set; }
}
```

![Example - Autocomplete control](https://github.com/darthmaure/blazor-autocomplete/blob/main/src/Images/Autocomplete_demo.gif "Selecting country by typing first letters")


## Token control
Token control allows to present something I call 'tags'. It is typically a list of string that describe the parent model. I was using similar control in my old WPF projects to tag entries and then to be able to seach the by keywords.
This control works in two modes
* Readonly - only boud collection of tokens is displayed
* Edit - there is also an input that allows to add new items or remove existing items

When in edit mode - use `;`, `Tab` or `Enter` to confirm value from input control to be added to the collection. Use `Backspace` when input control is empty to remove previous token (to the left) from the collection.


| Parameter     | Type | Default value | Descrption  |
| ------------- | -----|:-------------:| ------------|
| Tokens      | `IList<string>` | empty | List of tokens - _I do not why but Blazor requires bound model property to be also declared an IList<string>_ |
| TokensChanged | `EventCallback` | `null` | The callback required for two-way binding |
| IsReadonly | `bool` | `false` | A flag that sets the mode of control |
|TokensAutocomplete|`List<string>`|`null`|_to be implemented_|
| ExistingOnly|`bool`|`false`| When `TokensAutocomplete` is set and this flag is set - it allows user to add only items from the bound autocomplete collection|
|InputPlaceholder|`string`|empty|Placeholder for the inner input control|
|CssClass|`string`|`form-control`|Use this property with name of custom css class to style the root container. By default the container has white backgound and simple thin border|
|TokenItemCssClass|`string`|`token-item`|Use this param with name of custom css class to style the elements inside the control|

### Example
When in edit mode - place the control inside `EditForm`:
```csharp
<EditForm Model="@Model">
    <div class="form-group">
        <TokenInput InputPlaceholder="new tag..." @bind-Tokens="Model.Tags" />
    </div>
</EditForm>
```
The minimal code should be like:
```csharp
@code {
    private Book Model = new Book { Tags = new List<string> { "tag1", "tag2" } }
}
```
Where `Book` is a simple POCO class with some `IList<string>` `Tags` property:
```csharp
public class Book
{
   public IList<string> Tags { get; set; }
}
```

![Example - usage of TokenInput control](https://github.com/darthmaure/blazor-autocomplete/blob/main/src/Images/Tokens_demo.gif "Presenting and adding new tokens with keys: Enter, Tab or ';'")


Use custom css classes to style the control (root panel) and the items:
```css
.token-input-transparent {
    border: none;
    background-color: transparent;
}

.token-item-small {
    display: inline-block;
    padding: 0.75em 0.65em;
    font-size: 0.8rem;
    font-weight: 300;
    line-height: 1;
    color: #fff;
    text-align: center;
    white-space: nowrap;
    vertical-align: baseline;
    border-radius: 0.2rem;
    margin-left: 1px;
    margin-top: 1px;
    background-color: royalblue;
}

.token-item-small:hover {
    background-color: #f3a508
}
```

```csharp
<TokenInput IsReadonly="true" @bind-Tokens="book.Tags"
        CssClass="token-input-transparent"
        TokenItemCssClass="token-item-small" />
```


![Example - list of books with tags](https://github.com/darthmaure/blazor-autocomplete/blob/main/src/Images/Tokens_on_list_demo.gif "List of books with tags")

## How to use it
I am very new to Blazor, so I was unable to make it separated control lib. I will try to do it in some near future.
For now: simply copy the *.razor and *.razor.css files into your e.g. _Controls_ folder


## Known bugs or things to improve
* Clicking `Arrow Up` button on Keyboard on `Autocomplete` control popup moves caret to the beginning of input text
* No `Autocomplete` for `TokenIput` control

## Fixed issues
* Autocomplete popup now does not move down content below when opened. It overlaps the content (as a popup with higher z-index)
* Clicking autocomplete item with mouse buton now selects the value inside the input control