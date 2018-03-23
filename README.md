# Alexa Speech for .NET

<img src="https://teamcity.idevolutionlab.com/app/rest/builds/buildType:(id:Frameworks_AlexaSpeech_ReleaseBuild)/statusIcon.svg"/>

Generate Speech Synthesis Markup Language (SSML) for Amazon Alexa with .NET.

`Alexa Speech` for .NET is a library that will help you generate valid SSML for use with Amazon Alexa. Instead of using string concatenation or worring when using special characters you can use this library to avoid and eliminate these problems with the provided clean and easy to use fluent API.
This library is based on the [Fluent-ish SSML Generator](https://github.com/kevbite/Kevsoft.Ssml) library which generates SSML as per [Speech Synthesis Markup Language (SSML) Version 1.1](https://www.w3.org/TR/2010/REC-speech-synthesis11-20100907/) but it is optimized for usage with Amazon Alexa.

## Features

The library supports the following SSML tags:
* amazon:effect
* audio
* break
* emphasis
* p
* phoneme
* prosody
* s
* say-as - supports the following __interpret-as__ values and formats as per [Amazon Alexa Speech Synthesis Markup Language (SSML) Reference](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#say-as)
  * characters
  * spell-out
  * cardinal
  * number
  * ordinal
  * digits
  * fraction
  * unit
  * date
  * time
  * telephone
  * address
  * interjection
  * expletive
* speak
* sub
* w - note that the previous tags used from the `ivona` namespace in the attribute names are not supported
  * amazon:VB
  * amazon:VBD
  * amazon:NN
  * amazon:SENSE_1

## Getting Started

  `Alexa Speech` can be installed via package manager console by executing the following command:
```powershell
PM> Install-Package Alexa.Speech
```
or by using the dotnet CLI:
```bash
$ dotnet add package Alexa.Speech
```

## How To Use
Following are examples of how to use `Alexa Speech` to generate valid SSML with the supported tags for your Alexa Skills:
#### [amazon:effect](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#amazon-effect)
```csharp
string speech = new Speech()
    .Say("I want to tell you a secret.")
    .Say("I am not a real human.")
      .WithEffect(AmazonEffect.Whisper)
    .Say("Can you believe it?")
    .Build();
```
```xml
<speak>
    I want to tell you a secret. 
    <amazon:effect name="whispered">I am not a real human.</amazon:effect> 
    Can you believe it?
</speak>
```

#### [audio](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#audio)
```csharp
string speech = new Speech()
    .Say("Welcome to Car-Fu.")
    .Play("https://s3.amazonaws.com/ask-soundlibrary/transportation/amzn_sfx_car_accelerate_01.mp3")
    .Say("You can order a ride, or request a fare estimate. Which will it be?")
    .Build();
```
```xml
<speak>
    Welcome to Car-Fu. 
    <audio src="https://s3.amazonaws.com/ask-soundlibrary/transportation/amzn_sfx_car_accelerate_01.mp3" /> 
    You can order a ride, or request a fare estimate. 
    Which will it be?
</speak>
```

#### [break](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#break)
```csharp
string speech = new Speech()
    .Say("There will be 3 seconds pause here")
    .Pause()
        .For(TimeSpan.FromSeconds(3))
    .Say("then the speech continues.")
    .Build();
```
```xml
<speak>
    There will be 3 seconds pause here 
    <break time="3000ms" /> 
    then the speech continues.
</speak>
```

#### [emphasis](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#emphasis)
```csharp
string speech = new Speech()
    .Say("I already told you I")
    .Say("really like")
        .Emphasise(EmphasiseLevel.Strong)
    .Say("that person.")
    .Build();
```
```xml
<speak>
    I already told you I 
    <emphasis level="strong">really like</emphasis> 
    that person.
</speak>
```

#### [p](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#p)
```csharp
string speech = new Speech()
    .Say("This is the first paragraph. There should be a pause after this text is spoken.")
        .AsParagraph()
    .Say("This is the second paragraph.")
        .AsParagraph()
    .Build();
```
```xml
<speak>
    <p>This is the first paragraph. There should be a pause after this text is spoken.</p>
    <p>This is the second paragraph.</p>
</speak>
```

#### [phoneme](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#phoneme)
```csharp
string speech = new Speech()
    .Say("You say,")
    .Say("pecan")
        .AsPhoneme(PhoneticAlphabet.IPA, "pɪˈkɑːn")
    .Say("but I say,")
    .Say("pecan")
        .AsPhoneme(PhoneticAlphabet.IPA, "ˈpi.kæn")
    .Build();
```
```xml
<speak>
    You say, 
    <phoneme alphabet="ipa" ph="pɪˈkɑːn">pecan</phoneme> 
    but I say, 
    <phoneme alphabet="ipa" ph="ˈpi.kæn">pecan</phoneme>
</speak>
```

#### [prosody](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#prosody)
```csharp
string speech = new Speech()
    .Say("Normal volume for the first sentence.")
    .Say("Louder volume for the second sentence.")
        .WithVolume(SpeechVolume.ExtraLoud)
    .Say("When I wake up,")
    .Say("I speak quite slowly.")
        .WithRate(SpeechRate.ExtraSlow)
    .Say("I can speak with my normal pitch,")
    .Say("but also with a much higher pitch,")
        .WithPitch(SpeechPitch.ExtraHigh)
    .Say("and also")
    .Say("with a lower pitch.")
        .WithPitch(SpeechPitch.Low)
    .Build();
```
```xml
<speak>
    Normal volume for the first sentence. 
    <prosody volume="x-loud">Louder volume for the second sentence.</prosody> 
    When I wake up, <prosody rate="x-slow">I speak quite slowly.</prosody> 
    I can speak with my normal pitch, <prosody pitch="x-high">but also with a much higher pitch,</prosody> 
    and also <prosody pitch="low">with a lower pitch.</prosody>
</speak>
```

#### [s](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#s)
```csharp
string speech = new Speech()
    .Say("This is a sentence")
        .AsSentence()
    .Say("There should be a short pause before this second sentence")
        .AsSentence()
    .Say("This sentence ends with a period and should have the same pause.")
    .Build();
```
```xml
<speak>
    <s>This is a sentence</s>
    <s>There should be a short pause before this second sentence</s> 
    This sentence ends with a period and should have the same pause.
</speak>
```

#### [say-as](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#say-as)
```csharp
string speech = new Speech()
    .Say(251)
        .AsCardinal()
    .Say("spoken as a cardinal number.")
    .Say(251)
        .AsOrdinal()
    .Say("spoken as an ordinal number.")
    .Say(1098468341)
        .AsDigits()
    .Say("is how you spell each digit separately.")
    .Say("3/20")
        .AsFraction()
    .Say("is one common fraction.")
    .Say("1kg")
        .AsUnit()
    .Say("is how much apples I will buy.")
    .Say("100 John St.")
        .AsAddress()
    .Say("is the address where I live.")
    .Say("I think that this comment is kind of")
    .Say("ignorant.")
        .Expletive()
    .Say("1'47''")
        .AsTime()
    .Say("was the time I needed to write this sample.")
    .Say("Call")
    .Say("+46 (0)70 123 4567")
        .AsTelephone()
    .Say("to contact me.")
    .Say("abcdefg")
        .AsCharacters()
    .Say("this is how I spell.")
    .Say("gfedcba")
        .SpellOut()
    .Say("another way to do it.")
    .Say(new DateTime(2018, 3, 15))
    .Say("is today's date.")
    .Say("Wow,")
        .AsInterjection()
    .Say("this was a long sample.")
    .Build();
```
```xml
<speak>
    <say-as interpret-as="cardinal">251</say-as> spoken as a cardinal number. 
    <say-as interpret-as="ordinal">251</say-as> spoken as an ordinal number. 
    <say-as interpret-as="digits">1098468341</say-as> is how you spell each digit separately. 
    <say-as interpret-as="fraction">3/20</say-as> is one common fraction. 
    <say-as interpret-as="unit">1kg</say-as> is how much apples I will buy. 
    <say-as interpret-as="address">100 John St.</say-as> is the address where I live. 
    I think that this comment is kind of <say-as interpret-as="expletive">ignorant.</say-as>
    <say-as interpret-as="time">1'47''</say-as> was the time I needed to write this sample. 
    Call <say-as interpret-as="telephone">+46 (0)70 123 4567</say-as> to contact me. 
    <say-as interpret-as="characters">abcdefg</say-as> this is how I spell. 
    <say-as interpret-as="spell-out">gfedcba</say-as> another way to do it. 
    <say-as interpret-as="date">20180315</say-as> is today's date. 
    <say-as interpret-as="interjection">Wow,</say-as> this was a long sample.
</speak>
```

#### [speak](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#speak)
```csharp
string speech = new Speech()
    .Say("This is what Alexa sounds like without any SSML.")
    .Build();
```
```xml
<speak>
    This is what Alexa sounds like without any SSML.
</speak>
sub
```

#### [sub](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#sub)
```csharp
string speech = new Speech()
    .Say("My favorite chemical element is")
    .Say("Al")
        .AsAlias("aluminum")
    .Say("but Al prefers")
    .Say("Mg")
        .AsAlias("magnesium")
    .Say(".")
    .Build();
```
```xml
<speak>
    My favorite chemical element is <sub alias="aluminum">Al</sub> 
    but Al prefers <sub alias="magnesium">Mg</sub>.
</speak>
sub
```

#### [w](https://developer.amazon.com/docs/custom-skills/speech-synthesis-markup-language-ssml-reference.html#w)
```csharp
string speech = new Speech()
    .Say("The word")
    .Say("read")
        .AsCharacters()
    .Say("may be interpreted as either the present simple form")
    .Say("read")
        .PronounceAs(PronounceRole.Verb)
    .Say("or the past participle form")
    .Say("read")
        .PronounceAs(PronounceRole.PastParticiple)
    .Build();
```
```xml
<speak>
    The word <say-as interpret-as="characters">read</say-as> may be interpreted as either the present simple form 
    <w role="amazon:VB">read</w> or the past participle form <w role="amazon:VBD">read</w>
</speak>
sub
```
