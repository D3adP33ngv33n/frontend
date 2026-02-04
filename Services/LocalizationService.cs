using Microsoft.JSInterop;

namespace Nostdlib.Services;

public class LocalizationService : ILocalizationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Dictionary<string, Dictionary<string, string>> _resources;
    private string _currentLanguage = "en";

    public event Action? OnLanguageChanged;

    private static readonly List<LanguageInfo> SupportedLanguages = new()
    {
        new LanguageInfo("en", "English", "English"),
        new LanguageInfo("ru", "Russian", "Русский"),
        new LanguageInfo("hy", "Armenian", "\u0540\u0561\u0575\u0565\u0580\u0565\u0576")
    };

    public LocalizationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _resources = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = GetEnglishResources(),
            ["ru"] = GetRussianResources(),
            ["hy"] = GetArmenianResources()
        };
    }

    public string CurrentLanguage => _currentLanguage;

    public string this[string key]
    {
        get
        {
            if (_resources.TryGetValue(_currentLanguage, out var langResources) &&
                langResources.TryGetValue(key, out var value))
            {
                return value;
            }

            // Fallback to English
            if (_resources.TryGetValue("en", out var enResources) &&
                enResources.TryGetValue(key, out var enValue))
            {
                return enValue;
            }

            return key;
        }
    }

    public async void SetLanguage(string languageCode)
    {
        if (SupportedLanguages.Any(l => l.Code == languageCode) && _currentLanguage != languageCode)
        {
            _currentLanguage = languageCode;
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "language", languageCode);
            OnLanguageChanged?.Invoke();
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            var savedLanguage = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "language");
            if (!string.IsNullOrEmpty(savedLanguage) && SupportedLanguages.Any(l => l.Code == savedLanguage))
            {
                _currentLanguage = savedLanguage;
            }
        }
        catch
        {
            // localStorage might not be available
        }
    }

    public IEnumerable<LanguageInfo> GetSupportedLanguages() => SupportedLanguages;

    private static Dictionary<string, string> GetEnglishResources() => new()
    {
        // Navbar
        ["Nav.About"] = "About",
        ["Nav.Services"] = "Services",
        ["Nav.OpenSource"] = "Open Source",
        ["Nav.Careers"] = "Careers",
        ["Nav.Privacy"] = "Privacy",
        ["Nav.Contact"] = "Contact",

        // Hero
        ["Hero.Title"] = "SECURITY WITHOUT COMPROMISE",
        ["Hero.Subtitle"] = "Elite cybersecurity consulting for organizations that demand discretion. Proven expertise. Measurable outcomes. Absolute confidentiality.",
        ["Hero.CTA.Primary"] = "Start a Conversation",
        ["Hero.CTA.Secondary"] = "Explore Our Work",

        // About
        ["About.Title"] = "About Us",
        ["About.Principle1"] = "Layered defense as architectural foundation",
        ["About.Principle2"] = "Absolute client confidentiality",
        ["About.Principle3"] = "Intelligence-driven security operations",
        ["About.Principle4"] = "Continuous validation through adversarial testing",
        ["About.Principle5"] = "First-principles approach to complex problems",

        // Services
        ["Services.Title"] = "Services",
        ["Services.Subtitle"] = "Specialized capabilities for complex security challenges",

        // Careers
        ["Careers.Title"] = "Careers",
        ["Careers.Subtitle"] = "Build your career at the edge of security research",
        ["Careers.Requirements"] = "Requirements",
        ["Careers.Apply"] = "Apply via LinkedIn",
        ["Careers.Note"] = "Our selection process is rigorous by design. Qualified candidates receive direct outreach for initial conversations.",

        // Blog/Open Source
        ["Blog.Title"] = "OPEN SOURCE",
        ["Blog.Subtitle"] = "Open source tools and technical research from our team",
        ["Blog.KeyFeatures"] = "Key Features",
        ["Blog.Audience"] = "Audience:",
        ["Blog.ReadMore"] = "Read More",
        ["Blog.GitHub"] = "GitHub",

        // Privacy
        ["Privacy.Title"] = "Privacy Policy",
        ["Privacy.Commitment1"] = "Zero client disclosure without explicit consent",
        ["Privacy.Commitment2"] = "No third-party analytics or tracking",
        ["Privacy.Commitment3"] = "Encrypted communications as standard",
        ["Privacy.Commitment4"] = "Data retention limited to operational necessity",
        ["Privacy.Commitment5"] = "Right to deletion honored without exception",

        // Contact
        ["Contact.Title"] = "Initiate Contact",
        ["Contact.Subtitle"] = "Ready to discuss your security posture? Reach out through a secure channel below.",
        ["Contact.Note"] = "All initial communications are treated as confidential",

        // Footer
        ["Footer.Rights"] = "All rights reserved.",
        ["Footer.Tagline"] = "Security Beyond Perimeters",

        // Common
        ["Common.Loading"] = "Loading...",
        ["Common.Error"] = "An error occurred",
        ["NotFound.Message"] = "Sorry, there's nothing at this address."
    };

    private static Dictionary<string, string> GetRussianResources() => new()
    {
        // Navbar
        ["Nav.About"] = "О нас",
        ["Nav.Services"] = "Услуги",
        ["Nav.OpenSource"] = "Open Source",
        ["Nav.Careers"] = "Карьера",
        ["Nav.Privacy"] = "Конфиденциальность",
        ["Nav.Contact"] = "Контакты",

        // Hero
        ["Hero.Title"] = "БЕЗОПАСНОСТЬ БЕЗ КОМПРОМИССОВ",
        ["Hero.Subtitle"] = "Элитный консалтинг по кибербезопасности для организаций, требующих конфиденциальности. Проверенная экспертиза. Измеримые результаты. Абсолютная конфиденциальность.",
        ["Hero.CTA.Primary"] = "Начать разговор",
        ["Hero.CTA.Secondary"] = "Изучить нашу работу",

        // About
        ["About.Title"] = "О нас",
        ["About.Principle1"] = "Многоуровневая защита как архитектурная основа",
        ["About.Principle2"] = "Абсолютная конфиденциальность клиента",
        ["About.Principle3"] = "Операции безопасности на основе разведки",
        ["About.Principle4"] = "Непрерывная проверка через состязательное тестирование",
        ["About.Principle5"] = "Подход с первых принципов к сложным задачам",

        // Services
        ["Services.Title"] = "Услуги",
        ["Services.Subtitle"] = "Специализированные возможности для сложных задач безопасности",

        // Careers
        ["Careers.Title"] = "Карьера",
        ["Careers.Subtitle"] = "Постройте карьеру на переднем крае исследований безопасности",
        ["Careers.Requirements"] = "Требования",
        ["Careers.Apply"] = "Откликнуться через LinkedIn",
        ["Careers.Note"] = "Наш процесс отбора строг по замыслу. Квалифицированные кандидаты получают прямые приглашения для первоначальных бесед.",

        // Blog/Open Source
        ["Blog.Title"] = "ОТКРЫТЫЙ КОД",
        ["Blog.Subtitle"] = "Инструменты с открытым исходным кодом и технические исследования нашей команды",
        ["Blog.KeyFeatures"] = "Ключевые особенности",
        ["Blog.Audience"] = "Аудитория:",
        ["Blog.ReadMore"] = "Читать далее",
        ["Blog.GitHub"] = "GitHub",

        // Privacy
        ["Privacy.Title"] = "Политика конфиденциальности",
        ["Privacy.Commitment1"] = "Никакого раскрытия информации о клиенте без явного согласия",
        ["Privacy.Commitment2"] = "Никакой сторонней аналитики или отслеживания",
        ["Privacy.Commitment3"] = "Зашифрованные коммуникации как стандарт",
        ["Privacy.Commitment4"] = "Хранение данных ограничено операционной необходимостью",
        ["Privacy.Commitment5"] = "Право на удаление соблюдается без исключений",

        // Contact
        ["Contact.Title"] = "Связаться с нами",
        ["Contact.Subtitle"] = "Готовы обсудить вашу безопасность? Свяжитесь с нами через защищённый канал ниже.",
        ["Contact.Note"] = "Все первоначальные сообщения рассматриваются как конфиденциальные",

        // Footer
        ["Footer.Rights"] = "Все права защищены.",
        ["Footer.Tagline"] = "Безопасность за пределами периметра",

        // Common
        ["Common.Loading"] = "Загрузка...",
        ["Common.Error"] = "Произошла ошибка",
        ["NotFound.Message"] = "Извините, по этому адресу ничего нет."
    };

    private static Dictionary<string, string> GetArmenianResources() => new()
    {
        // Navbar
        ["Nav.About"] = "\u0544\u0565\u0580 \u0574\u0561\u057d\u056b\u0576",
        ["Nav.Services"] = "\u053e\u0561\u057c\u0561\u0575\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580",
        ["Nav.OpenSource"] = "\u0532\u0561\u0581 \u056f\u0578\u0564",
        ["Nav.Careers"] = "\u053f\u0561\u0580\u056b\u0565\u0580\u0561",
        ["Nav.Privacy"] = "\u0533\u0561\u0572\u057f\u0576\u056b\u0578\u0582\u0569\u0575\u0578\u0582\u0576",
        ["Nav.Contact"] = "\u053f\u0561\u057a",

        // Hero
        ["Hero.Title"] = "\u0531\u0546\u054e\u054f\u0531\u0546\u0533\u0548\u054d\u054f\u0545\u0548\u0552\u0546 \u0531\u054c\u0531\u0546\u0551 \u053f\u0548\u0544\u054a\u054c\u0548\u0544\u053b\u054d\u0546\u0535\u054c\u053b",
        ["Hero.Subtitle"] = "\u0537\u056c\u056b\u057f\u0561\u0580 \u056f\u056b\u0562\u0565\u0580\u0561\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0561\u0576 \u056d\u0578\u0580\u0570\u0580\u0564\u0561\u057f\u057e\u0578\u0582\u0569\u0575\u0578\u0582\u0576 \u056f\u0561\u0566\u0574\u0561\u056f\u0565\u0580\u057a\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580\u056b \u0570\u0561\u0574\u0561\u0580\u0589 \u0531\u057a\u0561\u0581\u0578\u0582\u0581\u057e\u0561\u056e \u0583\u0578\u0580\u0571\u0561\u0572\u056b\u057f\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0589 \u0549\u0561\u0583\u0565\u056c\u056b \u0561\u0580\u0564\u0575\u0578\u0582\u0576\u0584\u0576\u0565\u0580\u0589 \u0532\u0561\u0581\u0561\u0580\u0571\u0561\u056f \u0563\u0561\u0572\u057f\u0576\u056b\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0589",
        ["Hero.CTA.Primary"] = "\u054d\u056f\u057d\u0565\u056c \u0566\u0580\u0578\u0582\u0575\u0581",
        ["Hero.CTA.Secondary"] = "\u053e\u0561\u0576\u0578\u0569\u0561\u0576\u0561\u056c \u0574\u0565\u0580 \u0561\u0577\u056d\u0561\u057f\u0561\u0576\u0584\u0568",

        // About
        ["About.Title"] = "\u0544\u0565\u0580 \u0574\u0561\u057d\u056b\u0576",
        ["About.Principle1"] = "\u0547\u0565\u0580\u057f\u0561\u057e\u0578\u0580 \u057a\u0561\u0577\u057f\u057a\u0561\u0576\u0578\u0582\u0569\u0575\u0578\u0582\u0576 \u0578\u0580\u057a\u0565\u057d \u0573\u0561\u0580\u057f\u0561\u0580\u0561\u057a\u0565\u057f\u0561\u056f\u0561\u0576 \u0570\u056b\u0574\u0584",
        ["About.Principle2"] = "\u0540\u0561\u0573\u0561\u056d\u0578\u0580\u0564\u056b \u0562\u0561\u0581\u0561\u0580\u0571\u0561\u056f \u0563\u0561\u0572\u057f\u0576\u056b\u0578\u0582\u0569\u0575\u0578\u0582\u0576",
        ["About.Principle3"] = "\u0540\u0565\u057f\u0561\u056d\u0578\u0582\u0566\u0561\u056f\u0561\u0576 \u0570\u056b\u0574\u0561\u0576 \u057e\u0580\u0561 \u0561\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0561\u0576 \u0563\u0578\u0580\u056e\u0578\u0572\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580",
        ["About.Principle4"] = "\u0547\u0561\u0580\u0578\u0582\u0576\u0561\u056f\u0561\u056f\u0561\u0576 \u057d\u057f\u0578\u0582\u0563\u0578\u0582\u0574 \u0570\u0561\u056f\u0561\u057c\u0561\u056f\u0578\u0580\u0564\u0561\u056f\u0561\u0576 \u0569\u0565\u057d\u057f\u0561\u057e\u0578\u0580\u0574\u0561\u0576 \u0574\u056b\u057b\u0578\u0581\u0578\u057e",
        ["About.Principle5"] = "\u0531\u057c\u0561\u057b\u056b\u0576 \u057d\u056f\u0566\u0562\u0578\u0582\u0576\u0584\u0576\u0565\u0580\u056b\u0581 \u0574\u0578\u057f\u0565\u0581\u0578\u0582\u0574 \u0562\u0561\u0580\u0564 \u056d\u0576\u0564\u056b\u0580\u0576\u0565\u0580\u056b\u0576",

        // Services
        ["Services.Title"] = "\u053e\u0561\u057c\u0561\u0575\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580",
        ["Services.Subtitle"] = "\u0544\u0561\u057d\u0576\u0561\u0563\u056b\u057f\u0561\u0581\u057e\u0561\u056e \u0570\u0576\u0561\u0580\u0561\u057e\u0578\u0580\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580 \u0562\u0561\u0580\u0564 \u0561\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0561\u0576 \u0574\u0561\u0580\u057f\u0561\u0570\u0580\u0561\u057e\u0565\u0580\u0576\u0565\u0580\u056b \u0570\u0561\u0574\u0561\u0580",

        // Careers
        ["Careers.Title"] = "\u053f\u0561\u0580\u056b\u0565\u0580\u0561",
        ["Careers.Subtitle"] = "\u053f\u0561\u057c\u0578\u0582\u0581\u0565\u0584 \u0571\u0565\u0580 \u056f\u0561\u0580\u056b\u0565\u0580\u0561\u0576 \u0561\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0561\u0576 \u0570\u0565\u057f\u0561\u0566\u0578\u057f\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580\u056b \u0561\u057c\u0561\u057b\u0576\u0561\u0574\u0561\u057d\u0578\u0582\u0574",
        ["Careers.Requirements"] = "\u054a\u0561\u0570\u0561\u0576\u057b\u0576\u0565\u0580",
        ["Careers.Apply"] = "\u0534\u056b\u0574\u0565\u056c LinkedIn-\u056b \u0574\u056b\u057b\u0578\u0581\u0578\u057e",
        ["Careers.Note"] = "\u0544\u0565\u0580 \u0568\u0576\u057f\u0580\u0578\u0582\u0569\u0575\u0561\u0576 \u0563\u0578\u0580\u056e\u0568\u0576\u0569\u0561\u0581\u0568 \u056d\u056b\u057d\u057f \u0567 \u0576\u0561\u056d\u0561\u0563\u056e\u0574\u0561\u0574\u0562\u0589 \u0548\u0580\u0561\u056f\u0561\u057e\u0578\u0580\u057e\u0561\u056e \u0569\u0565\u056f\u0576\u0561\u056e\u0578\u0582\u0576\u0565\u0580\u0568 \u057d\u057f\u0561\u0576\u0578\u0582\u0574 \u0565\u0576 \u0578\u0582\u0572\u056b\u0572 \u0570\u0580\u0561\u057e\u0565\u0580\u0576\u0565\u0580 \u0576\u0561\u056d\u0576\u0561\u056f\u0561\u0576 \u0566\u0580\u0578\u0582\u0575\u0581\u0576\u0565\u0580\u056b \u0570\u0561\u0574\u0561\u0580\u0589",

        // Blog/Open Source
        ["Blog.Title"] = "\u0532\u0531\u0551 \u053f\u0548\u0534",
        ["Blog.Subtitle"] = "\u0532\u0561\u0581 \u056f\u0578\u0564\u056b \u0563\u0578\u0580\u056e\u056b\u0584\u0576\u0565\u0580 \u0587 \u057f\u0565\u056d\u0576\u056b\u056f\u0561\u056f\u0561\u0576 \u0570\u0565\u057f\u0561\u0566\u0578\u057f\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580 \u0574\u0565\u0580 \u0569\u056b\u0574\u056b\u0581",
        ["Blog.KeyFeatures"] = "\u0540\u056b\u0574\u0576\u0561\u056f\u0561\u0576 \u0570\u0561\u057f\u056f\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580",
        ["Blog.Audience"] = "\u0539\u056b\u0580\u0561\u056d\u0561\u057e\u056b\u0576\u0580\u0568\u055d",
        ["Blog.ReadMore"] = "\u053f\u0561\u0580\u0564\u0561\u056c \u0561\u057e\u0565\u056c\u056b\u0576",
        ["Blog.GitHub"] = "GitHub",

        // Privacy
        ["Privacy.Title"] = "\u0533\u0561\u0572\u057f\u0576\u056b\u0578\u0582\u0569\u0575\u0561\u0576 \u0584\u0561\u0572\u0561\u0584\u0561\u056f\u0561\u0576\u0578\u0582\u0569\u0575\u0578\u0582\u0576",
        ["Privacy.Commitment1"] = "\u0540\u0561\u0573\u0561\u056d\u0578\u0580\u0564\u056b \u0574\u0561\u057d\u056b\u0576 \u057f\u0565\u0572\u0565\u056f\u0561\u057f\u057e\u0578\u0582\u0569\u0575\u0561\u0576 \u0562\u0561\u0581\u0561\u056f\u0561\u0575\u0578\u0582\u0569\u0575\u0578\u0582\u0576 \u0561\u057c\u0561\u0576\u0581 \u0570\u0561\u0574\u0561\u0571\u0561\u0575\u0576\u0578\u0582\u0569\u0575\u0561\u0576",
        ["Privacy.Commitment2"] = "\u0548\u0579 \u0574\u056b \u0565\u0580\u0580\u0578\u0580\u0564 \u056f\u0578\u0572\u0574\u056b \u057e\u0565\u0580\u056c\u0578\u0582\u056e\u0561\u056f\u0561\u0576 \u056f\u0561\u0574 \u0570\u0565\u057f\u0584\u0576\u0578\u0582\u0574",
        ["Privacy.Commitment3"] = "\u0533\u0561\u0572\u057f\u0576\u0561\u0563\u0580\u057e\u0561\u056e \u0570\u0561\u0572\u0578\u0580\u0564\u0561\u056f\u0581\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580 \u0578\u0580\u057a\u0565\u057d \u057d\u057f\u0561\u0576\u0564\u0561\u0580\u057f",
        ["Privacy.Commitment4"] = "\u054f\u057e\u0575\u0561\u056c\u0576\u0565\u0580\u056b \u057a\u0561\u0570\u057a\u0561\u0576\u0578\u0582\u0574\u0568 \u057d\u0561\u0570\u0574\u0561\u0576\u0561\u0583\u0561\u056f\u057e\u0561\u056e \u0567 \u0563\u0578\u0580\u056e\u0561\u057c\u0576\u0561\u056f\u0561\u0576 \u0561\u0576\u0570\u0580\u0561\u056a\u0565\u0577\u057f\u0578\u0582\u0569\u0575\u0561\u0574\u0562",
        ["Privacy.Commitment5"] = "\u054b\u0576\u057b\u0574\u0561\u0576 \u056b\u0580\u0561\u057e\u0578\u0582\u0576\u0584\u0568 \u0570\u0561\u0580\u0563\u057e\u0578\u0582\u0574 \u0567 \u0561\u0576\u056d\u0578\u057d",

        // Contact
        ["Contact.Title"] = "\u053f\u0561\u057a\u057e\u0565\u056c \u0574\u0565\u0566 \u0570\u0565\u057f",
        ["Contact.Subtitle"] = "\u054a\u0561\u057f\u0580\u0561\u057d\u057f\u0556 \u0584\u0576\u0576\u0561\u0580\u056f\u0565\u056c \u0571\u0565\u0580 \u0561\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0561\u0576 \u057e\u056b\u0573\u0561\u056f\u0568\u0556 \u053f\u0561\u057a\u057e\u0565\u0584 \u0574\u0565\u0566 \u0570\u0565\u057f \u057d\u057f\u0578\u0580\u0587 \u0576\u0577\u057e\u0561\u056e \u0561\u057a\u0561\u0570\u0578\u057e \u0561\u056c\u056b\u0584\u0578\u057e\u0589",
        ["Contact.Note"] = "\u0532\u0578\u056c\u0578\u0580 \u0576\u0561\u056d\u0576\u0561\u056f\u0561\u0576 \u0570\u0561\u0572\u0578\u0580\u0564\u0561\u056f\u0581\u0578\u0582\u0569\u0575\u0578\u0582\u0576\u0576\u0565\u0580\u0568 \u0564\u056b\u057f\u0561\u0580\u056f\u057e\u0578\u0582\u0574 \u0565\u0576 \u0578\u0580\u057a\u0565\u057d \u0563\u0561\u0572\u057f\u0576\u056b",

        // Footer
        ["Footer.Rights"] = "\u0532\u0578\u056c\u0578\u0580 \u056b\u0580\u0561\u057e\u0578\u0582\u0576\u0584\u0576\u0565\u0580\u0568 \u057a\u0561\u0577\u057f\u057a\u0561\u0576\u057e\u0561\u056e \u0565\u0576\u0589",
        ["Footer.Tagline"] = "\u0531\u0576\u057e\u057f\u0561\u0576\u0563\u0578\u0582\u0569\u0575\u0578\u0582\u0576 \u057a\u0565\u0580\u056b\u0574\u0565\u057f\u0580\u056b\u0581 \u0564\u0578\u0582\u0580\u057d",

        // Common
        ["Common.Loading"] = "\u0532\u0565\u057c\u0576\u057e\u0578\u0582\u0574 \u0567...",
        ["Common.Error"] = "\u054d\u056d\u0561\u056c \u057f\u0565\u0572\u056b \u0578\u0582\u0576\u0565\u0581\u0561\u057e",
        ["NotFound.Message"] = "\u0546\u0565\u0580\u0565\u0581\u0565\u0584, \u0561\u0575\u057d \u0570\u0561\u057d\u0581\u0565\u0578\u0582\u0574 \u0578\u0579\u056b\u0576\u0579 \u0579\u056f\u0561\u0589"
    };
}
