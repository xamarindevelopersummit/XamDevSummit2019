# Unit Testing Prism Apps With Ease

Prism makes it easy to use dependency injection and keep our apps modular. This modularity is great for testing, but all that dependency injection magic can make our test code seem more complex and filled with a lot of ceremony just to create a testable instance of a ViewModel and its dependencies. I've learned to embrace magic and I'll share tips on how to take advantage of the same IoC container strategy used at runtime to make instantiating the class we want to test less painful. We'll tap into the life-cycle or our unit testing framework and combine a mocking framework and a custom lifetime manager for our IoC container to make sure each test has clean dependencies and keep us from ever directly instantiating our ViewModels or dependencies again. A great side-effect is that our tests become more resilient against changing dependencies that do not concern the test. Skip all the setup ceremony and get straight to the actual test.

# Duane Newman

## About

Duane is Co-Founder of Alien Arc Technologies, LLC where he focuses on creating apps targeting mobile devices, modern desktops, and the Internet of Things. As a Microsoft MVP and technology enthusiast with a passion for good software he strives to bring solutions that improve or eliminate costly duplication and repetitive processes so more important things can be done. He enjoys teaching others and speaking at conferences on topics ranging from DevOps to Xamarin. When not behind a computer screen he can be found sharing his love of SCUBA and all things underwater with new divers at the pool or through his underwater videos and photos at DLDAdventures.com.

## Contact

* [twitter](https://twitter.com/duanenewman)
* [blog](https://duanenewman.net)
* [GitHub](https://github.com/duanenewman)
* [linkedin](https://linkedin.com/in/duanenewman)

# Ease Library

Ease is a .Net library to ease unit testing through IoC containers and Mocking.

Ease is built on top of [NUnit](https://github.com/nunit) & [Moq](https://github.com/moq) and currently supports [DryIoc](https://github.com/dadhi/DryIoc) & [Unity](https://github.com/unitycontainer) IoC containers and has extensions with some basic support for [Prism.Forms](https://github.com/prismlibrary).

Our philosophy is to embrace the magic of DI and the IoC containers that we are already using in our app development to make testing easier to write and manage. 

# Installation

You can install the core IoC containerized test libraries from Nuget.org:

```
Install-Package Ease.NUnit.DryIoc.PrismForms 
```

```
Install-Package Ease.NUnit.Unity.PrismForms 
```

For Prism.Forms support:

```
Install-Package Ease.NUnit.DryIoc.PrismForms 
```

```
Install-Package Ease.NUnit.Unity.PrismForms 
```

# Source
 
The source is available on [GitHub](https://github.com/EaseLibrary).
