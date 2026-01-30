### [**[NoRWX] Proof-of-Concept**](blog/NoRWX/)
**Problem:** When writing shellcode in C, only the .text section is available after compilation, so global constants and string literals cannot be used because they are placed in .rdata or .data.

**Solution:** Minimize usage of construction that will cause generation of the data in .rdata or .data, with moving string literals to stack. To create stack‑based strings, represent the string as a character array stored in a local variable. This solution also obfuscates strings:
  
**Technologies Used:** x86-64 assembly, C.  
**Status:** Experimental, for research and educational use.  
**Audience:** Security researchers, low-level programmers.  

---

### [**[cpp-pic] Modern C++ Approach to Zero-Dependency, Position-Independent Code Generation**](blog/cpp-pic/) 
**Purpose:** Fully position-independent code implementation in C++ with manual API resolution.  
**Key Features:**
 
 - No imports, no standard library, position-independent code, no fixed addresses
 - Embed and retrieve strings without fixed addresses  
 - Direct TEB/PEB access   
 - Works on x86 and x64 
 - PEB walking

**Technologies:** C++, Windows internals, Linux systemcalls  
**Status:** Experimental  
**Audience:** Systems programmers, low-level developers, security researchers

---


### [**[c-pic] Fully position-independent code implementation in C**](blog/c-pic/) 
**Purpose:** Fully position-independent code implementation in C with manual API resolution.  
**Key Features:**
 
 - No imports, no standard library, position-independent code, no fixed addresses
 - Embed and retrieve strings without fixed addresses  
 - Direct TEB/PEB access   
 - Works on x86 and x64 
 - PEB walking

**Technologies:** C, Windows internals, Linux systemcalls  
**Status:** Experimental  
**Audience:** Systems programmers, low-level developers, security researchers

---

### [**[EC] Armenian Encoding Converter**](blog/EC/)  
**Purpose:** Converts Armenian text between multiple encodings (ANSI, Unicode, ArmSCII, etc.).  
**Key Features:**

 - Supports multiple Armenian encodings  
 - Batch conversion  
 - Word/Excel converters (TXT/DOC/DOCX/XLS/XLSX)  

**Technologies:** C#, .NET 9  
**Status:** Beta  
**Audience:** Linguists, developers dealing with Armenian text, localization engineers

---