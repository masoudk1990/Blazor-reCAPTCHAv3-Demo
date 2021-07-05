# Blazor-reCAPTCHAv3-Demo
A demo to demonstrate how to use reCAPTCHA v3 in Blazor applications.

Mainly inspired from [this](https://github.com/mdwaseelahmed/reCAPTCHADemo) repository. The major difference is that this demo is demonstrating how to reverify generated tokens in the back-end.

## Important files:

### Client > wwwroot > Index.html:
Here we have a `script` tag to google reCaptcha api and a function to generate token:

```javascript
<script src="https://www.google.com/recaptcha/api.js?render={your site key}"></script>
<script>
    runCaptcha = function (actionName) {
        return new Promise((resolve, reject) => {
            grecaptcha.ready(function () {
                grecaptcha.execute('{your site key}', { action: 'submit' }).then(function (token) {
                    resolve(token);
                });
            });
        });
    };
</script>
```
 
### Client > Pages > Index.razor:
This is where we use reCAPTCHA in our front-end
 
### Client > Pages > GoogleCaptchaService.cs:
We use this service to call an API in the back-end to reverify generated token:
 
```c#
public virtual async Task<GoogleCaptchaResponseDto> reVerify(string token)
{
    var response = await httpClient.GetAsync("api/authentication/reverifycaptcha?token=" + token);
    var captchaResponse = JsonSerializer.Deserialize<GoogleCaptchaResponseDto>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return captchaResponse;
}
```
 
### Server > Controllers > AuthenticationController.cs:
This is where we reverify the generated token in backend:

```c#
[HttpGet("reverifycaptcha")]
public async Task<IActionResult> RreVerifyCaptcha(string token)
{
    var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("secret", _reCaptchaSettings.secretKey), new KeyValuePair<string, string>("response", token) });
    var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify", content);

    var jsonString = await response.Content.ReadAsStringAsync();
    return Ok(jsonString);
}
```

### Shared > Entities > DataTransferObjects
This is where our Dtos exists.
